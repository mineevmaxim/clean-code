using Markdown.Enums;

namespace Markdown.TokenTypes;

public class BoldMarkdownToken(int position, string value) : MarkdownToken(position, value), IPairedMarkdownTokenType
{
    public override MarkdownTokenName Name => MarkdownTokenName.Bold;
    public string OpenTag => "__";
    public string CloseTag => "__";
    public bool CanBeWithoutCloseTag => false;
}