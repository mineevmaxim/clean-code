using Markdown.Enums;

namespace Markdown.TokenTypes;

public class ItalicMarkdownToken(int position, string value) : MarkdownToken(position, value), IPairedMarkdownTokenType
{
    public override MarkdownTokenName Name => MarkdownTokenName.Italic;
    public string OpenTag => "_";
    public string CloseTag => "_";
    public bool CanBeWithoutCloseTag => false;
}