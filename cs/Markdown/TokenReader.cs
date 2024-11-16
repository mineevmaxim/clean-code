using System.Text;

namespace Markdown;

public static class TokenReader
{
    public static Token? ReadToken(string line, int startIndex, TokenReaderParams parameters)
    {
        var index = startIndex;
        var currentStart = new List<char>();

        while (currentStart.Count < parameters.StartTag.Length && index < line.Length)
            currentStart.Add(line[index++]);

        var isStartTag = currentStart.SequenceEqual(parameters.StartTag);

        if (!isStartTag) return null;
        if (isStartTag && line[index] == ' ' && !parameters.CanBeSpaceAfterStartTag) return null;
        var currentEnd = new Queue<char>();
        var sb = new StringBuilder();

        while (index < line.Length)
        {
            currentEnd.Enqueue(line[index]);
            var isCloseTag = currentEnd.SequenceEqual(parameters.EndTag);
            var previousIsSpace = line[index - 1] == ' ';
            var valueIsEndSubarray = sb.ToString().ToCharArray().All(c => parameters.EndTag.Contains(c));
            var isEndOfParagraphOrLine = (line[index] == '\n' || index == line.Length - 1);

            if (isCloseTag)
            {
                if (valueIsEndSubarray && parameters.CanBeEmpty) break;
                if (valueIsEndSubarray) return null;
                if (parameters.CanBeSpaceBeforeCloseTag) break;
                if (!parameters.CanBeSpaceBeforeCloseTag && !previousIsSpace) break;
            }

            if (!parameters.CanBeWithoutCloseTag && isEndOfParagraphOrLine) return null;
            if (currentEnd.Count == parameters.EndTag.Length) currentEnd.Dequeue();
            sb.Append(line[index]);
            index++;
        }

        return new Token(sb.ToString(), startIndex, index - startIndex + 1);
    }
}