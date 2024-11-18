namespace Markdown;

public class StrongToken: Token
{
    public readonly IEnumerable<Token>? ChildTokens;
    
    public StrongToken(string value, int position, int length, IEnumerable<Token>? children = null) : base(value, position, length)
        => ChildTokens = children;
    
    public static StrongToken? ReadStrong(string line, int startIndex)
    {
        var reader = new StrongTokenReader();
        var token = reader.ReadStrongWithChildren(line, startIndex);
        return token is null ? null : new StrongToken(token.Value, startIndex, token.Length, null);
    }
}