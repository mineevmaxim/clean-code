namespace Markdown;

public interface IParser
{
    string Parse(IEnumerable<IToken> tokens);
}