namespace Markdown;

public interface ISingleMarkdownTokenType : IMarkdownTokenType
{
    string OpenTag { get; }
}