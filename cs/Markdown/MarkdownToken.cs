namespace Markdown;

public class MarkdownToken(int position, string value, IMarkdownTokenType type) : IToken
{
    public int Position { get; } = position;
    public string Value { get; } = value;
    public int Length => Value.Length;
    public IMarkdownTokenType Type { get; } = type;

    public int GetIndexToNextToken() => Position + Length;
}