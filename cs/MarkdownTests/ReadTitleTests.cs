using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests;

public class ReadTitleTests
{
    [TestCase("# title", 0, "title", 7, 1)]
    [TestCase("## title", 0, "title", 8, 2)]
    [TestCase("### title", 0, "title", 9, 3)]
    [TestCase("#### title", 0, "title", 10, 4)]
    [TestCase("##### title", 0, "title", 11, 5)]
    [TestCase("###### title", 0, "title", 12, 6)]
    public void ReadHeader_ReturnsHeaderToken_WithCorrectLevel(string line, int startIndex, string expectedValue,
        int expectedLength, int expectedLevel)
    {
        var expected = new HeaderToken(expectedValue, startIndex, expectedLength, expectedLevel);
        var token = HeaderToken.ReadHeader(line, startIndex);
        token.Should().Be(expected);
    }
    
    [TestCase("", 0, TestName = "Empty line")]
    [TestCase("#tag", 0, TestName = "Without space after #")]
    [TestCase("####### title", 0, TestName = "Header level greater than 6")]
    [TestCase("123 # title", 0, TestName = "Invalid start index")]
    public void ReadHeader_ReturnsNull_WithInvalidSyntaxOrArgs(string line, int startIndex)
    {
        var token = HeaderToken.ReadHeader(line, startIndex);
        token.Should().BeNull();
    }
    
    [TestCase("with text before # title", 17, "title", 7)]
    [TestCase("# title\n with text after", 0, "title", 7)]
    [TestCase("with text before # title\n and after", 17, "title", 7)]
    public void ReadHeader_ReturnsCorrectToken_WithCorrectArgs(string line, int startIndex, string expectedValue,
        int expectedLength)
    {
        var expected = new HeaderToken(expectedValue, startIndex, expectedLength, 1);
        var token = HeaderToken.ReadHeader(line, startIndex);
        token.Should().Be(expected);
    }
}