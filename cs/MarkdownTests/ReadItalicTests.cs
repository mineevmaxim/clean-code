using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests;

public class ReadItalicTests
{
    [TestCase("", 0, TestName = "Empty line")]
    [TestCase("_without close tag", 0, TestName = "Without close tag in text")]
    [TestCase("_without close tag\n in paragraph", 0, TestName = "Without close tag in paragraph")]
    [TestCase("_ space after start tag_", 0, TestName = "Space after start tag")]
    [TestCase("__", 0, TestName = "Empty string between tags")]
    [TestCase("_text _", 0, TestName = "With space before close tag without correct close tag")]
    [TestCase("_text_", 2, TestName = "With invalid start")]
    [TestCase("_text1_2", 0, TestName = "Close tag between numbers")]
    [TestCase("_text1_2_", 0, TestName = "Close tag after numbers")]
    [TestCase(@"_text\_", 0, TestName = "With escaped close tag")]
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
    [TestCase("_with numbers 1_200 inside_", 0, "with numbers 1_200 inside", 27)]
    public void ReadItalic_ReturnsToken_WithCorrectArgs(string line, int start, string expectedValue,
        int expectedLength)
    {
        var expected = new ItalicToken(expectedValue, start, expectedLength);
        var token = ItalicToken.ReadItalic(line, start);
        token.Should().Be(expected);
    }

    [TestCase(@"_te\_xt_", 0, "te_xt", 8, TestName = "Escaped close tag should be saved in result")]
    [TestCase(@"_te\axt_", 0, @"te\axt", 8, TestName = "Escaped any symbol should be saved in result")]
    [TestCase(@"_te\\xt_", 0, @"te\xt", 8, TestName = "Escaped slash should be saved in result")]
    public void ReadItalic_ReturnsToken_WithEscapedSymbols(string line, int start, string expectedValue,
        int expectedLength)
    {
        var expected = new ItalicToken(expectedValue, start, expectedLength);
        var token = ItalicToken.ReadItalic(line, start);
        token.Should().Be(expected);
    }
}