using Markdown.Enums;

namespace Markdown;

public interface IToken
{
    MarkdownTokenName Name { get; }
    int Position { get; }
    int Length { get; }
    string Value { get; }
    int GetIndexToNextToken();
    bool Is(MarkdownTokenName type);
}