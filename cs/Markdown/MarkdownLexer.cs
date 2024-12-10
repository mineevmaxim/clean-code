using System.Text;
using Markdown.Tokens;

namespace Markdown;

public class MarkdownLexer : ILexer
{
    private int position;
    private readonly List<Token> tokens = [];
    private const string DoubleGround = "__";
    private const string Ground = "_";
    private const string Escape = "\\";
    private const char GroundChar = '_';
    private const char SharpChar = '#';
    private const char EscapeChar = '\\';
    private const char NewLineChar = '\n';
    private const char SpaceChar = ' ';
    private readonly char[] escapedChars = [SharpChar, GroundChar, EscapeChar, NewLineChar];

    public List<Token> Tokenize(string input)
    {
        position = 0;
        var nestingStack = new Stack<string>();

        while (position < input.Length)
        {
            switch (input[position])
            {
                case SpaceChar:
                    ParseSpaceAndAdvance();
                    break;
                case NewLineChar:
                    ParseNewLineAndAdvance(nestingStack);
                    break;
                case EscapeChar:
                    ParseEscapeAndAdvance(input);
                    break;
                case GroundChar:
                    ParseItalicOrBoldAndAdvance(input, nestingStack);
                    break;
                case SharpChar:
                    ParseHeadingAndAdvance(input);
                    break;
                default:
                    ParseTextAndAdvance(input);
                    break;
            }
        }

        return tokens;
    }

    private void ParseSpaceAndAdvance() => tokens.Add(new SpaceToken(position++));

    private void ParseHeadingAndAdvance(string input)
    {
        if (NextIsSpace(input) && IsStartOfParagraph(input)) tokens.Add(new HeadingToken(position++));
        else tokens.Add(new TextToken(position, $"{SharpChar}"));
        position++;
    }

    private void ParseTextAndAdvance(string input)
    {
        var value = new StringBuilder();
        var start = position;
        var endChars = new[] { SharpChar, GroundChar, NewLineChar, EscapeChar, SpaceChar };
        while (position < input.Length && !endChars.Contains(input[position]) && !CurrentIsDigit(input))
            value.Append(input[position++]);

        if (value.Length > 0) tokens.Add(new TextToken(start, value.ToString()));
        if (position < input.Length && CurrentIsDigit(input)) ParseNumberAndAdvance(input);
    }


    private void ParseNumberAndAdvance(string input)
    {
        var sb = new StringBuilder();
        var start = position;
        while (position < input.Length && (CurrentIsDigit(input) || input[position] == GroundChar))
            sb.Append(input[position++]);
        tokens.Add(new NumberToken(start, sb.ToString()));
    }

    private void ParseItalicOrBoldAndAdvance(string input, Stack<string> stack)
    {
        var isDoubleGround = NextIsGround(input);
        var isTripleGround = NextIsDoubleGround(input);
        var isSingleGround = !isTripleGround && !isDoubleGround;
        if (stack.Count == 0) ParseItalicOrBoldAndAdvanceWhenStackEmpty(isSingleGround, isTripleGround, stack);
        else if (stack.Count == 1)
            ParseItalicOrBoldAndAdvanceWhenStackHasOne(isSingleGround, isDoubleGround, isTripleGround, stack);
        else if (stack.Count == 2) ParseItalicOrBoldAndAdvanceWhenStackHasTwo(isSingleGround, isTripleGround, stack);
    }

    private void ParseItalicOrBoldAndAdvanceWhenStackEmpty(bool isSingleGround, bool isTripleGround,
        Stack<string> stack)
    {
        if (isSingleGround)
        {
            ParseItalicAndAdvance();
            stack.Push(Ground);
            return;
        }

        ParseBoldAndAdvance();
        stack.Push(DoubleGround);
        if (!isTripleGround) return;
        ParseItalicAndAdvance();
        stack.Push(Ground);
    }

    private void ParseItalicOrBoldAndAdvanceWhenStackHasOne(bool isSingleGround, bool isDoubleGround,
        bool isTripleGround,
        Stack<string> stack)
    {
        switch (stack.Peek())
        {
            case DoubleGround when isSingleGround:
                ParseItalicAndAdvance();
                stack.Push(Ground);
                break;
            case DoubleGround:
            {
                if (isTripleGround) ParseItalicAndAdvance();
                ParseBoldAndAdvance();
                stack.Pop();
                break;
            }
            case Ground:
            {
                if (isTripleGround)
                {
                    ParseBoldAndAdvance();
                    ParseItalicAndAdvance();
                }
                else if (isDoubleGround)
                {
                    tokens.Add(new TextToken(position, DoubleGround));
                    position += 2;
                }
                else ParseItalicAndAdvance();

                stack.Pop();
                break;
            }
        }
    }

    private void ParseItalicOrBoldAndAdvanceWhenStackHasTwo(bool isSingleGround, bool isTripleGround,
        Stack<string> stack)
    {
        if (isSingleGround)
        {
            ParseItalicAndAdvance();
            stack.Pop();
            return;
        }

        if (isTripleGround) ParseItalicAndAdvance();
        ParseBoldAndAdvance();

        stack.Pop();
        stack.Pop();
    }

    private void ParseBoldAndAdvance()
    {
        tokens.Add(new BoldToken(position));
        position += 2;
    }

    private void ParseItalicAndAdvance()
    {
        tokens.Add(new ItalicToken(position));
        position++;
    }

    private void ParseNewLineAndAdvance(Stack<string> stack)
    {
        tokens.Add(new NewLineToken(position));
        stack.Clear();
        position++;
    }

    private void ParseEscapeAndAdvance(string input)
    {
        if (position + 1 >= input.Length)
        {
            tokens.Add(new TextToken(position++, Escape));
            return;
        }

        if (NextIsDoubleGround(input))
        {
            tokens.Add(new TextToken(position, DoubleGround));
            position += 3;
            return;
        }

        var next = input[position + 1];
        tokens.Add(escapedChars.Contains(next)
            ? new TextToken(position, next.ToString())
            : new TextToken(position, Escape + next));
        position += 2;
    }

    private bool NextIsDoubleGround(string input) =>
        position + 2 < input.Length && input[position + 1] == GroundChar && input[position + 2] == GroundChar;

    private bool NextIsSpace(string input) => position + 1 < input.Length && input[position + 1] == SpaceChar;
    private bool NextIsGround(string input) => position + 1 < input.Length && input[position + 1] == GroundChar;
    private bool CurrentIsDigit(string input) => char.IsDigit(input[position]);

    private bool IsStartOfParagraph(string input) =>
        position == 0 || position > 0 && input[position - 1] == NewLineChar;
}