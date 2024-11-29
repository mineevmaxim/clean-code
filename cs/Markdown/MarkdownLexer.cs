using Markdown.TokenTypes;

namespace Markdown;

public class MarkdownLexer : ILexer
{
    public int Position { get; private set; }
    private List<MarkdownToken> Tokens { get; } = [];
    private int position;
    
    public IEnumerable<IToken> Tokenize(string input)
    {
        throw new NotImplementedException();
    }

    private BoldMarkdownToken? TryParseBold(string input)
    {
        throw new NotImplementedException();
    }
    
    private ItalicMarkdownToken? TryParseItalic(string input)
    {
        throw new NotImplementedException();
    }
    
    private HeadingMarkdownToken? TryParseHeading(string input)
    {
        throw new NotImplementedException();
    }
    
    private TextMarkdownToken? TryParseText(string input)
    {
        throw new NotImplementedException();
    }
}