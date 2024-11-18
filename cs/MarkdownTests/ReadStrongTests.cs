using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests;

public class ReadStrongTests
{
    [TestCase("", 0, TestName = "Empty line")]
    [TestCase("____", 0, TestName = "Empty string between tags")]
    [TestCase("__text_", 0, TestName = "Without close tag")]
    [TestCase(@"__text\__", 0, TestName = "With escaped close tag")]
    [TestCase("__ text__", 0, TestName = "Space after start tag")]
    [TestCase("__text __", 0, TestName = "Space before close tag")]
    [TestCase("__text__", 2, TestName = "Invalid start index")]
    [TestCase("__text\n", 0, TestName = "Without close tag in paragraph")]
    public void ReadStrong_ReturnsNull_WithInvalidArgs(string line, int startIndex)
    {
        var token = StrongToken.ReadStrong(line, startIndex);
        token.Should().BeNull();
    }
    
    [TestCase("__text__", 0, "text", 8)]
    [TestCase("with text before __text__", 17, "text", 8)]
    [TestCase("__text__ with text after", 0, "text", 8)]
    [TestCase("__with spaces inside__", 0, "with spaces inside", 22)]
    [TestCase("__with spaces _ inside__", 0, "with spaces _ inside", 24)]
    [TestCase("__with numbers 1_200 inside__", 0, "with numbers 1_200 inside", 29)]
    public void ReadStrong_ReturnsToken_WithCorrectArgs(string line, int start, string expectedValue,
        int expectedLength)
    {
        var expected = new StrongToken(expectedValue, start, expectedLength);
        var token = StrongToken.ReadStrong(line, start);
        token.Should().Be(expected);
    }
    
    [TestCase(@"__te\__xt__", 0, "te__xt", 11, TestName = "Escaped close tag should be saved in result")]
    [TestCase(@"__te\axt__", 0, @"te\axt", 10, TestName = "Escaped any symbol should be saved in result")]
    [TestCase(@"__te\\xt__", 0, @"te\xt", 10, TestName = "Escaped slash should be saved in result")]
    public void ReadStrong_ReturnsToken_WithEscapedSymbols(string line, int start, string expectedValue,
        int expectedLength)
    {
        var expected = new StrongToken(expectedValue, start, expectedLength);
        var token = StrongToken.ReadStrong(line, start);
        token.Should().Be(expected);
    }
    
    [Test]
    public void ReadStrong_ReturnsToken_WithOneItalicChild()
    {
        var expectedChildren = new Token[] {new ItalicToken("text", 5, 6)};
        var expected = new StrongToken("text _text_ text", 0, 20, expectedChildren);
        var token = StrongToken.ReadStrong("__text _text_ text__", 0);
        token?.ChildTokens?.SequenceEqual(expectedChildren).Should().BeTrue();
        token.Should().Be(expected);
    }
    
    [Test]
    public void ReadStrong_ReturnsToken_WithTwoItalicChild()
    {
        var expectedChildren = new Token[] {new ItalicToken("text", 5, 6), new ItalicToken("text", 13, 6)};
        var expected = new StrongToken("text _text_ _text_ text", 0, 27, expectedChildren);
        var token = StrongToken.ReadStrong("__text _text_ _text_ text__", 0);
        token?.ChildTokens?.SequenceEqual(expectedChildren).Should().BeTrue();
        token.Should().Be(expected);
    }
}