using Markdown.Enums;

namespace Markdown.TokenTypes;

public class HeaderMarkdownTokenType : ISingleMarkdownTokenType
{
    public MarkdownTokenName Name => MarkdownTokenName.Header;
    public string OpenTag => "# ";
}