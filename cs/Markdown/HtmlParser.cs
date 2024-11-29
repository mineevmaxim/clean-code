using Markdown.Enums;

namespace Markdown;

public class HtmlParser : IParser
{
    private static readonly Dictionary<MarkdownTokenName, string> TokenToHtmlTag = new ()
    {
        { MarkdownTokenName.Bold, "strong" },
        { MarkdownTokenName.Italic, "em" },
        { MarkdownTokenName.Heading, "h1" }
    };
    
    public string Parse(IEnumerable<IToken> tokens)
    {
        throw new NotImplementedException();
    }
}