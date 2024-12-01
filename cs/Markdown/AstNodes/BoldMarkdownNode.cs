using System.Text;
using Markdown.Enums;

namespace Markdown.AstNodes;

public class BoldMarkdownNode(string content) : MarkdownNode(content), IMarkdownNodeWithChildren
{
    public override MarkdownNodeName Type => MarkdownNodeName.Bold;
    public List<MarkdownNode> Children { get; } = [];
}