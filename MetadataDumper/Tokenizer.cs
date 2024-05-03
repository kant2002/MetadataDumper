namespace MetadataDumper;

public class Tokenizer
{
    public IEnumerable<string> ParseTokens(string text)
    {
        if (text.Length == 0 || text == "AspNetCore")
        {
            yield return text;
            yield break;
        }

        if (text.Contains('.'))
        {
            foreach (var item in text.Split('.').SelectMany(ParseTokens))
            {
                yield return item;
            }

            yield break;
        }

        string? accum = null;
        for (var i = 0; i < text.Length; i++)
        {
            if (char.IsDigit(text[i]) || text[i] == '_' || text[i] == '`' || text[i] == '<' || text[i] == '>')
            {
                if (accum is not null)
                    yield return accum;
                accum = null;
            }
            else if (char.IsUpper(text[i]))
            {
                if (accum is not null)
                    yield return accum;
                accum = text[i].ToString();
            }
            else
            {
                accum += text[i];
            }
        }

        if (accum is not null)
            yield return accum;
    }
}
