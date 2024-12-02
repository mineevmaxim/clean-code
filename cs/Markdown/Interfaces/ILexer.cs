namespace Markdown;

public interface ILexer
{
    List<IToken> Tokenize(string input);
}