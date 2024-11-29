namespace Markdown.AstNodes;

public class BoldMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public List<MarkdownNode> Children { get; } = [];
}