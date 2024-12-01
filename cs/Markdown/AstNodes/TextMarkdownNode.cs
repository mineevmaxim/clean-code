using Markdown.Enums;

namespace Markdown.AstNodes;

public class TextMarkdownNode(string content) : MarkdownNode(content)
{
    public override MarkdownNodeName Type => MarkdownNodeName.Text;
}