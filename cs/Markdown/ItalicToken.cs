using System.Text;

namespace Markdown;

public class ItalicToken: Token
{
    public const char Tag = '_';
    private readonly List<ItalicToken> insideTokens = new List<ItalicToken>();
        
    public ItalicToken(string value, int position, int length) : base(value, position, length)
    {
    }

    public static ItalicToken? ReadItalic(string line, int startIndex)
    {
        if (line[startIndex] != Tag) return null;
        if (line.Length <= startIndex + 1)
            return null;
        var index = startIndex + 1;
        if (line[index] == ' ' || line[index] == Tag) return null;

        var sb = new StringBuilder();
        while (index < line.Length)
        {
            if (line[index] == Tag && line[index-1] != ' ') break;
            if (line[index] == '\n' || index == line.Length - 1) return null;
            sb.Append(line[index]);
            index++;
        }
        
        return new ItalicToken(sb.ToString(), startIndex, index - startIndex + 1);
    }
}