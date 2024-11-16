namespace Markdown;

public class Token
{
    public readonly int Length;

    public readonly int Position;
    public readonly string Value;

    public Token(string value, int position, int length)
    {
        Position = position;
        Length = length;
        Value = value;
    }

    public override bool Equals(object? obj) => obj is Token token && Equals(token);

    private bool Equals(Token other)
        => Length == other.Length && Position == other.Position && string.Equals(Value, other.Value);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Length;
            hashCode = (hashCode * 397) ^ Position;
            hashCode = (hashCode * 397) ^ (Value.GetHashCode());
            return hashCode;
        }
    }

    public int GetIndexNextToToken() => Position + Length;

    public override string ToString()
    {
        var value = $"[{Value}]";
        return $"{value,-10} Position={Position:##0} Length={Length}";
    }
}