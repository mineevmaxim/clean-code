using System.Text;

namespace Markdown;

public class HeaderToken : Token
{
    public readonly int TitleLevel;
    public const char Tag = '#';

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

        if (line[++index] != ' ')
            return null;

        index++;

        var sb = new StringBuilder();
        while (index < line.Length && line[index] != '\n')
        {
            sb.Append(line[index]);
            index++;
        }
        
        return new HeaderToken(sb.ToString(), startIndex, index - startIndex, titleLevel);
    }
}