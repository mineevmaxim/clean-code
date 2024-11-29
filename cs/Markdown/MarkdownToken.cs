using Markdown.Enums;

namespace Markdown;

public abstract class MarkdownToken(int position, string value) : IToken
{
    public int Position { get; } = position;
    public string Value { get; } = value;
    public int Length => Value.Length;
    public virtual MarkdownTokenName Name { get; }

    public int GetIndexToNextToken() => Position + Length;
}