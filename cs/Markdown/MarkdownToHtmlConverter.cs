using Markdown.AstNodes;

namespace Markdown;

public class MarkdownToHtmlConverter(ILexer lexer, IParser parser)
{
    public ILexer Lexer { get; } = lexer;
    public IParser Parser { get; } = parser;

    public string Convert(string input)
    {
        var tokens = Lexer.Tokenize(input);
        var enumerable = tokens.ToList();
        var ast = Parser.Parse(enumerable);
        return ConvertAstToHtml(ast);
    }

    private string ConvertAstToHtml(MarkdownNode ast)
    {
        throw new NotImplementedException();
    }
}