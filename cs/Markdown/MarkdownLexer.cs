using System.Text;
using Markdown.Tokens;

namespace Markdown;

public class MarkdownLexer : ILexer
{
    private int position;
    private readonly List<IToken> tokens = [];

    public List<IToken> Tokenize(string input)
    {
        position = 0;
        var end = input.Length;
        var stack = new Stack<(string tag, int position)>();

        while (InBoundary(position))
        {
            switch (input[position])
            {
                case ' ':
                    tokens.Add(new SpaceToken(position++));
                    break;
                case '\n':
                    ParseNewLineAndAdvance(stack);
                    break;
                case '\\':
                    ParseEscapeAndAdvance(input);
                    break;
                case '_':
                    ParseItalicOrBoldAndAdvance(input, stack);
                    break;
                case '#':
                    ParseHeadingAndAdvance(input, stack);
                    break;
                default:
                    ParseTextAndAdvance(input);
                    break;
            }
        }

        return tokens;

        bool InBoundary(int i) => i < end;
    }

    private bool IsStartOfParagraph(string input) => position == 0 || position > 0 && input[position - 1] == '\n';

    private void ParseHeadingAndAdvance(string input, Stack<(string tag, int position)> stack)
    {
        if (position + 1 < input.Length && input[position + 1] == ' ' && IsStartOfParagraph(input))
        {
            tokens.Add(new HeadingToken(position));
            position += 2;
            stack.Push(("# ", position));
        }
        else
        {
            tokens.Add(new TextToken(position, "#"));
            position++;
        }
    }

    private void ParseTextAndAdvance(string input)
    {
        var sb = new StringBuilder();
        var start = position;
        var endChars = new[] { '#', '_', '\n', '\\', ' ' };
        while (position < input.Length && !endChars.Contains(input[position]) && !char.IsDigit(input[position]))
            sb.Append(input[position++]);

        if (sb.Length > 0) tokens.Add(new TextToken(start, sb.ToString()));
        if (position < input.Length && char.IsDigit(input[position])) ParseNumberAndAdvance(input);
    }

    private void ParseNumberAndAdvance(string input)
    {
        var sb = new StringBuilder();
        var start = position;
        while (position < input.Length && (char.IsDigit(input[position]) || input[position] == '_'))
            sb.Append(input[position++]);
        tokens.Add(new NumberToken(start, sb.ToString()));
    }

    private void ParseItalicOrBoldAndAdvance(string input, Stack<(string tag, int position)> stack)
    {
        var canBeBold = position + 1 < input.Length && input[position + 1] == '_';
        if (stack.Count > 0 && stack.Peek().tag == "__" && canBeBold) ParseBoldAndAdvance(stack);
        else if (stack.Count > 0 && stack.Peek().tag == "_") ParseItalicAndAdvance(stack);
        else if (canBeBold) ParseBoldAndAdvance(stack);
        else ParseItalicAndAdvance(stack);
    }

    private void ParseBoldAndAdvance(Stack<(string tag, int position)> stack)
    {
        if (stack.Count == 0 || stack.Count > 0 && (stack.Peek().tag == "# " || stack.Peek().tag == "_"))
            stack.Push(("__", position));
        else if (stack.Count > 0 && stack.Peek().tag == "__")
            stack.Pop();
        else throw new Exception("Не рассмотрел какой-то случай в жирном");
        
        tokens.Add(new BoldToken(position));
        position += 2;
    }

    private void ParseItalicAndAdvance(Stack<(string tag, int position)> stack)
    {
        if (stack.Count == 0 || stack.Count > 0 && (stack.Peek().tag == "__" || stack.Peek().tag == "# "))
            stack.Push(("_", position));
        else if (stack.Count > 0 && stack.Peek().tag == "_")
            stack.Pop();
        else throw new Exception("Не рассмотрел какой-то случай в курсиве");
        tokens.Add(new ItalicToken(position));
        position++;
    }

    private void ParseNewLineAndAdvance(Stack<(string tag, int position)> stack)
    { 
        tokens.Add(new NewLineToken(position));
        stack.Clear();
        position++;
    }

    private void ParseEscapeAndAdvance(string input)
    {
        if (position + 1 >= input.Length)
        {
            tokens.Add(new TextToken(position, "\\"));
            return;
        }

        if (input[position + 1] == '#')
        {
            tokens.Add(new TextToken(position, "#"));
            position += 2;
        }
        else if (position + 2 < input.Length && input[position + 1] == '_' && input[position + 2] == '_')
        {
            tokens.Add(new TextToken(position, "__"));
            position += 3;
        }
        else if (input[position + 1] == '_')
        {
            tokens.Add(new TextToken(position, "_"));
            position += 2;
        }
        else if (input[position + 1] == '\\')
        {
            tokens.Add(new TextToken(position, "\\"));
            position += 2;
        }
        else
        {
            tokens.Add(new TextToken(position, input[position].ToString() + input[position + 1]));
            position += 2;
        }
    }
}