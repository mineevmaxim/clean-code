using Markdown.Enums;

namespace Markdown.TokenTypes;

public class TextMarkdownTokenType : IMarkdownTokenType
{
    public MarkdownTokenName Name => MarkdownTokenName.Text;
}