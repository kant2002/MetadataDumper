using dnlib.DotNet;
using MetadataRewriter;
using System.Globalization;

if (args.Length < 2)
{
    Console.WriteLine("MetadataRewriter mdfile targetmdfile [rewritersdirectory]");
    return;
}

var assemblyFile = args[0];
var targetFile = args[1];
var targetDirectory = args.ElementAtOrDefault(2) ?? Directory.GetCurrentDirectory();
ModuleContext modCtx = ModuleDef.CreateModuleContext();
ModuleDefMD module = ModuleDefMD.Load(assemblyFile, modCtx);
var metadata = module.Metadata;
var rewriter = new Rewriter(module);

string typeRenamesPath = Path.Combine(targetDirectory, "TypeRenames.csv");
if (Path.Exists(typeRenamesPath))
{
    var typeRewriteRules = LoadDictionaryRules(typeRenamesPath);
    rewriter.RewriteTypes(typeRewriteRules);
}

if (Path.Exists(Path.Combine(targetDirectory, "TypeRenamesByName.csv")))
{
    var typeRewriteRulesByName = LoadRules(Path.Combine(targetDirectory, "TypeRenamesByName.csv"));
    rewriter.RewriteTypes(typeRewriteRulesByName.ToDictionary(_ => _.OldName, _ => _.NewName));
}

if (Path.Exists(Path.Combine(targetDirectory, "MethodRenames.csv")))
{
    var methodRewriteRules = LoadDictionaryRules(typeRenamesPath);
    rewriter.RewriteMethods(methodRewriteRules);
}

module.Write(targetFile);
Console.WriteLine($"Rewritten metadata for the assembly {assemblyFile} saved to {targetFile}");

// Function which read list of MetadataRewriteRule from specific CSV file
List<MetadataRewriteRule> LoadRules(string filePath)
{
    var lines = File.ReadAllLines(filePath);
    var rules = new List<MetadataRewriteRule>();
    foreach (var line in lines.Skip(1)) // Skip header
    {
        var parts = line.Split(',');
        uint tokenHandle;
        if (parts.Length == 3 &&
            (uint.TryParse(parts[0], out tokenHandle) || uint.TryParse(parts[0][2..], NumberStyles.AllowHexSpecifier, null, out tokenHandle)) &&
            !string.IsNullOrWhiteSpace(parts[1]) &&
            !string.IsNullOrWhiteSpace(parts[2]))
        {
            rules.Add(new MetadataRewriteRule
            {
                TokenHandle = tokenHandle,
                OldName = parts[1],
                NewName = parts[2]
            });
        }
    }
    return rules;
}

// Function which read list of MetadataRewriteRule from specific CSV file
Dictionary<uint, string> LoadDictionaryRules(string filePath)
{
    var lines = File.ReadAllLines(filePath);
    var rules = new Dictionary<uint, string>();
    foreach (var line in lines.Skip(1)) // Skip header
    {
        var parts = line.Split(',');
        uint tokenHandle;
        if (parts.Length == 2 &&
            (uint.TryParse(parts[0], out tokenHandle) || uint.TryParse(parts[0][2..], NumberStyles.AllowHexSpecifier, null, out tokenHandle)) &&
            !string.IsNullOrWhiteSpace(parts[1]))
        {
            rules.Add(tokenHandle, parts[1]);
        }
    }
    return rules;
}

class MetadataRewriteRule
{
    public uint TokenHandle { get; init; }
    public required string OldName { get; set; }
    public required string NewName { get; set; }
}