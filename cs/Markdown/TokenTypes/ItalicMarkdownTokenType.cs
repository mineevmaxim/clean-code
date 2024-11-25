using Markdown.Enums;

namespace Markdown.TokenTypes;

public class ItalicMarkdownTokenType : IPairedMarkdownTokenType
{
    public MarkdownTokenName Name => MarkdownTokenName.Italic;
    public string OpenTag => "_";
    public string CloseTag => "_";
    public bool CanBeWithoutCloseTag => false;
}