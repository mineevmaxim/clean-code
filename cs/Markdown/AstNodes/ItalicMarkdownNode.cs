namespace Markdown.AstNodes;

public class ItalicMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public List<MarkdownNode> Children { get; } = [];
}