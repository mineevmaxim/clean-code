namespace Markdown;

public interface IToken
{
    int Position { get; }
    int Length { get; }
    string Value { get; }
    int GetIndexToNextToken();
}