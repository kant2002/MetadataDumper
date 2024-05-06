using dnlib.DotNet;
using MetadataDumper;
using Microsoft.Data.Sqlite;

if (args.Length < 1)
{
    Console.WriteLine("MetadataDumper mdfile [targetdirectory]");
    return;
}

var assemblyFile = args[0];
var targetDirectory = args.ElementAtOrDefault(1) ?? Directory.GetCurrentDirectory();
ModuleContext modCtx = ModuleDef.CreateModuleContext();
ModuleDefMD module = ModuleDefMD.Load(assemblyFile, modCtx);
var metadata = module.Metadata;
var exporter = new MetadataExporter(metadata);
exporter.DumpMetadata(targetDirectory);
Console.WriteLine($"Metadata for the assembly {assemblyFile} saved to {targetDirectory}");
var normalizedStrings = exporter.CollectedStrings
        .Where(_ => !IsCompilerGenerated(_));
var uniqueStrings = normalizedStrings.Select(FilterNames).OrderBy(_ => _).Distinct();
var auxFolder = Path.Combine(targetDirectory, "auxillary");
Directory.CreateDirectory(auxFolder);
File.WriteAllLines(Path.Combine(auxFolder, "MetadataStrings.csv"), uniqueStrings);
var tokenizer = new Tokenizer();
var wordsStatistics = normalizedStrings.SelectMany(tokenizer.ParseTokens)
    .Select(FilterNames)
    .OrderBy(_ => _).GroupBy(_ => _)
    .Where(_ => _.Key.Length > 1)
    .Select(_ => new WordCount(_.Key, _.Count())).ToList();
File.WriteAllLines(Path.Combine(auxFolder, "MetadataWords.csv"), new[] { "Word,Count" }.Union(wordsStatistics
    .Select(_ => $"{_.Word},{_.Count}")).ToArray());

using var connection = new SqliteConnection("Data Source=en.sqlite3");
connection.Open();

var command = connection.CreateCommand();
command.CommandText =
        """
        SELECT part_of_speech
        FROM entry
        WHERE (written_rep = $word OR written_rep = LOWER($word)) AND lexentry LIKE 'eng/%'
        """;
command.Parameters.Add("$word", SqliteType.Text);

File.WriteAllLines(Path.Combine(auxFolder, "MetadataWordsAdv.csv"), new[] { "Word,POS,Count" }.Union(wordsStatistics.Select(EnchancePartOfSpeech)
.Select(_ => $"{_.Word},{_.PartOfSpeech},{_.Count}")).ToArray());


bool IsCompilerGenerated(string metadataString)
{
    return metadataString.Contains("<")
        || metadataString.Contains(".")
        || metadataString.Contains("@")
        || metadataString.Contains("~")
        || metadataString.Contains("?")
        || metadataString.Contains("$")
        || metadataString.Contains("{")
        || (metadataString.Length > 0 && char.IsDigit(metadataString[0]));
}

string FilterNames(string name)
{
    if (name.StartsWith("get_") || name.StartsWith("set_"))
        name = name[4..];
    if (name.StartsWith("s_"))
        name = name[2..];
    name = name.TrimStart('_');
    if (name.Length > 0)
        if (!char.IsUpper(name[0]))
            return char.ToUpper(name[0]) + name.Substring(1);
    return name;
}

WordPosCount EnchancePartOfSpeech(WordCount wordCount)
{
    command.Parameters["$word"].Value = wordCount.Word;
    var partOfSpeech = command.ExecuteScalar();
    return new WordPosCount(wordCount.Word, partOfSpeech?.ToString() ?? "", wordCount.Count);
}

record WordCount(string Word, int Count);
record WordPosCount(string Word, string PartOfSpeech, int Count);