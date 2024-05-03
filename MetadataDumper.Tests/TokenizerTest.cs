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
        }
    }
}