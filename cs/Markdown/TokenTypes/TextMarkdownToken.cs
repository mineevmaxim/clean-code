using Markdown.Enums;

namespace Markdown.TokenTypes;

public class TextMarkdownToken(int position, string value) : MarkdownToken(position, value), IMarkdownTokenType
{
    public override MarkdownTokenName Name => MarkdownTokenName.Text;
}