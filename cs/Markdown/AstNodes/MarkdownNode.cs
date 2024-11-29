namespace Markdown.AstNodes;

public abstract class MarkdownNode(string content)
{
    public string Content { get; } = content;
}