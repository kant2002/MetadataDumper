namespace MetadataDumper.Tests;

[TestClass]
public class TokenizerTest
{
    [DataTestMethod]
    [DynamicData(nameof(GetTokenizerTests))]
    public void TokenizeTextIntoWords(string text, string[] expected)
    {
        var tokenizer = new Tokenizer();

        var tokens = tokenizer.ParseTokens(text);

        CollectionAssert.AreEquivalent(expected, tokens.ToArray());
    }

    [DataTestMethod]
    [DynamicData(nameof(GetSingleTokens))]
    public void NonTokenizableWordsShouldBeKeptIntact(string text)
    {
        var tokenizer = new Tokenizer();

        var tokens = tokenizer.ParseTokens(text);

        CollectionAssert.AreEquivalent(new[] { text }, tokens.ToArray());
    }

    public static IEnumerable<object[]> GetSingleTokens
    { 
        get
        {
            yield return new[] { "AspNetCore" };
        }
    }

    public static IEnumerable<object[]> GetTokenizerTests
    {
        get
        {
            yield return new object[] { "", new[] { "" } };
            yield return new object[] { "Single", new[] { "Single" } };
            yield return new object[] { "TwoWords", new[] { "Two", "Words" } };
            yield return new object[] { "System.Collections.Generic", new[] { "System", "Collections", "Generic" } };
            yield return new object[] { "Action`1", new[] { "Action" } };
            yield return new object[] { "Tcp4", new[] { "Tcp" } };
            yield return new object[] { "TContainerBuilder", new[] { "T", "Container", "Builder" } };
            yield return new object[] { "IPAddress", new[] { "IP", "Address" } };
            yield return new object[] { "IP", new[] { "IP" } };
            yield return new object[] { "DesignerURL", new[] { "Designer", "URL" } };
            yield return new object[] { "BASESCRIPTRECORD", new[] { "BASESCRIPTRECORD" } };
            yield return new object[] { "BASECOORDFORMAT2", new[] { "BASECOORDFORMAT" } };
            yield return new object[] { "CMAP_FORMAT0", new[] { "CMAP", "FORMAT" } };
            yield return new object[] { "Char_Glyph_Map_List", new[] { "Char", "Glyph", "Map", "List" } };
        }
    }
}