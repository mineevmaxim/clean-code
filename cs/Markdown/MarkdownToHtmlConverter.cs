namespace Markdown;

public class MarkdownToHtmlConverter(ILexer lexer, IParser parser)
{
    public ILexer Lexer { get; } = lexer;
    public IParser Parser { get; } = parser;

    public string Convert(string input)
    {
        var tokens = Lexer.Tokenize(input);
        return Parser.Parse(tokens);
    }
}