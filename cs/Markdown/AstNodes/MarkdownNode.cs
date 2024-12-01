using Markdown.Enums;

namespace Markdown.AstNodes;

public abstract class MarkdownNode(string content)
{
    public abstract MarkdownNodeName Type { get; }
    public string Content { get; } = content;
}