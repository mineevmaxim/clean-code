using Markdown.Enums;

namespace Markdown.AstNodes;

public class RootMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public override MarkdownNodeName Type => MarkdownNodeName.Root;
    public List<MarkdownNode> Children { get; } = [];
}