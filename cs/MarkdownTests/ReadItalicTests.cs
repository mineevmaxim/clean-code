using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests;

public class ReadItalicTests
{
    [TestCase("_without close tag", 0, TestName = "Without close tag in text")]
    [TestCase("_without close tag\n in paragraph", 0, TestName = "Without close tag in paragraph")]
    [TestCase("_ space after start tag_", 0, TestName = "Space after start tag")]
    [TestCase("__", 0, TestName = "Empty string between tags")]
    [TestCase("_text _", 0, TestName = "With space before close tag without correct close tag")]
    [TestCase("_text_", 2, TestName = "With invalid start")]
    public void ReadItalic_ReturnsNull_WithInvalidArgs(string line, int start)
    {
        var token = ItalicToken.ReadItalic(line, start);
        token.Should().BeNull();
    }

    [TestCase("_text_", 0, "text", 6)]
    [TestCase("with text before _text_", 17, "text", 6)]
    [TestCase("_text_ with text after", 0, "text", 6)]
    [TestCase("_with spaces inside_", 0, "with spaces inside", 20)]
    [TestCase("_with spaces _ inside_", 0, "with spaces _ inside", 22)]
    public void ReadItalic_ReturnsToken_WithCorrectArgs(string line, int start, string expectedValue,
        int expectedLength)
    {
        var expected = new ItalicToken(expectedValue, start, expectedLength);
        var token = ItalicToken.ReadItalic(line, start);
        token.Should().Be(expected);
    }
}