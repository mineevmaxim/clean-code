using FluentAssertions;
using Markdown;
using NUnit.Framework;

namespace MarkdownTests;

public class ConvertToHtmlTests
{
    [TestCase("text", "<em>text</em>")]
    [TestCase("with space", "<em>with space</em>")]
    public void ConvertToHtml_ReturnsHtml_WhenItalic(string value, string expected)
    {
        var token = new ItalicToken(value, 0, value.Length + 2);
        var actual = Md.ConvertToHtml(token);
        actual.Should().Be(expected);
    }

    [TestCase("text", "<strong>text</strong>")]
    [TestCase("with space", "<strong>with space</strong>")]
    public void ConvertToHtml_ReturnsHtml_WhenStrong(string value, string expected)
    {
        var token = new StrongToken(value, 0, value.Length + 2);
        var actual = Md.ConvertToHtml(token);
        actual.Should().Be(expected);
    }

    [Test]
    public void ConvertToHtml_ReturnsHtml_WhenStrongWithOneChild()
    {
        var token = new StrongToken("text _text_ text", 0, 18, [new ItalicToken("text", 5, 6)]);
        var actual = Md.ConvertToHtml(token);
        Console.WriteLine(actual);
        actual.Should().Be("<strong>text <em>text</em> text</strong>");
    }

    [Test]
    public void ConvertToHtml_ReturnsHtml_WhenStrongWithTwoChild()
    {
        var token = new StrongToken("text _text_ _abc_ text", 0, 24,
            [new ItalicToken("text", 5, 6), new ItalicToken("abc", 12, 5)]);
        var actual = Md.ConvertToHtml(token);
        Console.WriteLine(actual);
        actual.Should().Be("<strong>text <em>text</em> <em>abc</em> text</strong>");
    }

    [TestCase("text", 1, "<h1>text</h1>")]
    [TestCase("text", 2, "<h2>text</h2>")]
    [TestCase("text", 3, "<h3>text</h3>")]
    [TestCase("text", 4, "<h4>text</h4>")]
    [TestCase("text", 5, "<h5>text</h5>")]
    [TestCase("text", 6, "<h6>text</h6>")]
    public void ConvertToHtml_ReturnsHtml_WhenHeader(string value, int level, string expected)
    {
        var token = new HeaderToken(value, 0, value.Length + 2, level);
        var actual = Md.ConvertToHtml(token);
        actual.Should().Be(expected);
    }
}