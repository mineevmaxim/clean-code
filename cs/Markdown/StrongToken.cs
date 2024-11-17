namespace Markdown;

public class StrongToken: Token
{
    private const string Tag = "__";
    private static readonly TokenReaderParams Parameters;

    static StrongToken()
        => Parameters = new TokenReaderParams("__", "__", false, false, false);
    
    public StrongToken(string value, int position, int length) : base(value, position, length)
    {
    }
    
    public static StrongToken? ReadStrong(string line, int startIndex)
    {
        var token = TokenReader.ReadToken(line, startIndex, Parameters);
        return token is null ? null : new StrongToken(token.Value, startIndex, token.Length);
    }
}