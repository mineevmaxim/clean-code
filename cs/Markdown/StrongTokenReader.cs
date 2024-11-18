namespace Markdown;

public class StrongTokenReader
{
    private readonly List<Token> childTokens = new();
    private static readonly TokenReaderParams Parameters;

    static StrongTokenReader()
        => Parameters = new TokenReaderParams("__", "__", false, false, false);

    public StrongToken? ReadStrongWithChildren(string line, int startIndex)
    {
        var token = TokenReader.ReadToken(line, startIndex, Parameters);
        if (token is null) return null;
        for (var i = 0; i < token.Value.Length; i++)
        {
            if (token.Value[i] != ItalicToken.Tag) continue;
            var childToken = ItalicToken.ReadItalic(token.Value, i);
            if (childToken == null) continue;
            childTokens.Add(childToken);
            i = childToken.GetIndexNextToToken();
        }

        return new StrongToken(token.Value, startIndex, token.Length, childTokens);
    }
}