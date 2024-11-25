namespace Markdown;

public interface IPairedMarkdownTokenType : IMarkdownTokenType
{
    string CloseTag { get; }
    bool CanBeWithoutCloseTag { get; }
}