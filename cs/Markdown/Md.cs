using System.Text;

namespace Markdown;

public class Md
{
    /*
     * Принимает в качестве аргумента текст в markdown-подобной разметке, и возвращает строку с html-кодом
     */
    public string Render(string markdown)
    {
        return "";
    }

    public static Token? ReadHeader(string line, int startIndex)
    {
        if (line[startIndex] != '#') return null;
        if (line.Length <= startIndex + 1)
            return null;
        var index = startIndex;
        var titleLevel = 1;
        while (line[index + 1] == '#')
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