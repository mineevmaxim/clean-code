using Markdown.Enums;

namespace Markdown.TokenTypes;

public class HeadingMarkdownToken(int position, string value) : MarkdownToken(position, value), ISingleMarkdownTokenType
{
    public override MarkdownTokenName Name => MarkdownTokenName.Heading;
    public string OpenTag => "# ";
}