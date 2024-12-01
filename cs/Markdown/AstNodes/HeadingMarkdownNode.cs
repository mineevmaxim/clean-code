using Markdown.Enums;

namespace Markdown.AstNodes;

public class HeadingMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public override MarkdownNodeName Type => MarkdownNodeName.Heading;
    public List<MarkdownNode> Children { get; } = [];
}