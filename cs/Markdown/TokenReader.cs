using System.Text;

namespace Markdown;

public static class TokenReader
{
    private static readonly char[] EscapableSymbols = { '_', '\\' };

    public static Token? ReadToken(string line, int startIndex, TokenReaderParams parameters)
    {
        var index = startIndex;
        var currentStart = new List<char>();

        while (currentStart.Count < parameters.StartTag.Length && index < line.Length) currentStart.Add(line[index++]);

        var isStartTag = currentStart.SequenceEqual(parameters.StartTag);
        if (!isStartTag) return null;
        if (isStartTag && line[index] == ' ' && !parameters.CanBeSpaceAfterStartTag) return null;
        var currentEnd = new Queue<char>();
        var currentValue = new StringBuilder();
        var isEscapable = false;

        while (index < line.Length)
        {
            if (isEscapable)
            {
                isEscapable = false;
                if (!EscapableSymbols.Contains(line[index])) currentValue.Append('\\');
                currentValue.Append(line[index++]);
                if (index == line.Length && !parameters.CanBeWithoutCloseTag) return null;
                continue;
            }

            if (line[index] == '\\' && !isEscapable)
            {
                isEscapable = true;
                index++;
                continue;
            }

            currentEnd.Enqueue(line[index]);
            var isCloseTag = currentEnd.SequenceEqual(parameters.EndTag);
            var previousIsSpace = line[index - currentEnd.Count] == ' ';
            var valueIsEndSubarray = currentValue.ToString().ToCharArray().All(c => parameters.EndTag.Contains(c));
            var isEndOfParagraphOrLine = (line[index] == '\n' || index == line.Length - 1);
            var isDigitsAround = char.IsDigit(line[index - 1]) || index <= line.Length - 2 && char.IsDigit(line[index + 1]);

            if (isCloseTag)
            {
                if (isDigitsAround && !parameters.CloseTagCanBeBetweenNumbers) goto Continue;
                if (valueIsEndSubarray && parameters.CanBeEmpty) break;
                if (valueIsEndSubarray) return null;
                if (parameters.CanBeSpaceBeforeCloseTag) break;
                if (!parameters.CanBeSpaceBeforeCloseTag && !previousIsSpace) break;
            }

            Continue:
            if (!parameters.CanBeWithoutCloseTag && isEndOfParagraphOrLine) return null;
            if (currentEnd.Count == parameters.EndTag.Length) currentEnd.Dequeue();
            currentValue.Append(line[index]);
            index++;
        }

        for (var i = 0; i < parameters.EndTag.Length - 1; i++) currentValue.Remove(currentValue.Length - 1, 1);
        return new Token(currentValue.ToString(), startIndex, index - startIndex + 1);
    }
}