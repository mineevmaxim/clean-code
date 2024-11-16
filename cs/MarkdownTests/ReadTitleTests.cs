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
        var token = Md.ReadHeader(line, startIndex);
        token.Should().Be(expected);
    }
    
    [TestCase("#tag", 0, TestName = "Without space after #")]
    [TestCase("####### title", 0, TestName = "Header level greater than 6")]
    public void ReadTitle_ReturnsNull_WithInvalidSyntax(string line, int startIndex)
    {
        var token = Md.ReadHeader(line, startIndex);
        token.Should().BeNull();
    }
    
    [TestCase("# title", 0, "title", 7, 1)]
    [TestCase("## title", 0, "title", 8, 2)]
    [TestCase("### title", 0, "title", 9, 3)]
    [TestCase("#### title", 0, "title", 10, 4)]
    [TestCase("##### title", 0, "title", 11, 5)]
    [TestCase("###### title", 0, "title", 12, 6)]
    public void ReadTitle_ReturnsTitleToken_WithCorrectLevel2(string line, int startIndex, string expectedValue,
        int expectedLength, int expectedLevel)
    {
        var expected = new HeaderToken(expectedValue, startIndex, expectedLength, expectedLevel);
        var token = Md.ReadHeader(line, startIndex);
        token.Should().Be(expected);
    }
}