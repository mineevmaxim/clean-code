namespace Markdown;

public class HeaderToken : Token
{
    public readonly int TitleLevel;
    private const char Tag = '#';
    private static readonly TokenReaderParams Parameters;

    static HeaderToken()
        => Parameters = new TokenReaderParams("# ", "\n", true, true, true);

    public HeaderToken(string value, int position, int length, int level = 1) : base(value, position, length)
        => TitleLevel = level;

    public override bool Equals(Token other)
        => base.Equals(other) && other is HeaderToken token && TitleLevel == token.TitleLevel;

    public static Token? ReadHeader(string line, int startIndex)
    {
        if (line[startIndex] != Tag) return null;
        if (line.Length <= startIndex + 1)
            return null;
        var index = startIndex;
        var titleLevel = 1;
        while (line[index + 1] == Tag)
        {
            index++;
            titleLevel++;
            if (titleLevel > 6) return null;
        }

        var token = TokenReader.ReadToken(line, startIndex + titleLevel - 1, Parameters);
        return token is null
            ? null
            : new HeaderToken(token.Value, startIndex, token.Length + titleLevel - 2, titleLevel);
    }
}