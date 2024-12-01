using Markdown.Enums;

namespace Markdown.AstNodes;

public class ItalicMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public override MarkdownNodeName Type => MarkdownNodeName.Italic;
    public List<MarkdownNode> Children { get; } = [];
}