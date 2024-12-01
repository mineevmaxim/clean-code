using Markdown.AstNodes;

namespace Markdown;

public interface IParser
{
    RootMarkdownNode Parse(List<IToken> tokens);
}