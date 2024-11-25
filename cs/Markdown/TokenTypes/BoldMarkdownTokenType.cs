using Markdown.Enums;

namespace Markdown.TokenTypes;

public class BoldMarkdownTokenType : IPairedMarkdownTokenType
{
    public MarkdownTokenName Name => MarkdownTokenName.Bold;
    public string OpenTag => "__";
    public string CloseTag => "__";
    public bool CanBeWithoutCloseTag => false;
}