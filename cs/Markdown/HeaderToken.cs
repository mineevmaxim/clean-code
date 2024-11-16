namespace Markdown;

public class HeaderToken : Token
{
    public readonly int TitleLevel;

    public HeaderToken(string value, int position, int length, int level = 1) : base(value, position, length)
        => TitleLevel = level;

    public override bool Equals(Token other)
        => base.Equals(other) && other is HeaderToken token && TitleLevel == token.TitleLevel;
}