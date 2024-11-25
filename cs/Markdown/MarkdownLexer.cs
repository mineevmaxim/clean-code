namespace Markdown;

public class MarkdownLexer : ILexer
{
    public int Position { get; private set; }
    private List<MarkdownToken> Tokens { get; } = [];
    
    public IEnumerable<IToken> Tokenize(string input)
    {
        throw new NotImplementedException();
    }
}