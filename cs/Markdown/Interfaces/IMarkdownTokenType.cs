using Markdown.Enums;

namespace Markdown;

public interface IMarkdownTokenType
{
    MarkdownTokenName Name { get; }
}