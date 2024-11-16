namespace Markdown;

public class ItalicToken : Token
{
    public const char Tag = '_';
    private readonly List<ItalicToken> insideTokens = new List<ItalicToken>();
    private static readonly TokenReaderParams Parameters;

    static ItalicToken()
        => Parameters = new TokenReaderParams("_", "_", false, false);

    public ItalicToken(string value, int position, int length) : base(value, position, length)
    {
    }

    public static ItalicToken? ReadItalic(string line, int startIndex)
    {
        var token = TokenReader.ReadToken(line, startIndex, Parameters);
        return token is null ? null : new ItalicToken(token.Value, startIndex, token.Length);
    }
}