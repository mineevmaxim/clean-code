namespace Markdown.AstNodes;

public class HeadingMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public List<MarkdownNode> Children { get; } = [];
}