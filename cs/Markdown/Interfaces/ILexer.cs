namespace Markdown;

public interface ILexer
{
    IEnumerable<IToken> Tokenize(string input);
}