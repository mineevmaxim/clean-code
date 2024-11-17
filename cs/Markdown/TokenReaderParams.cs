namespace Markdown;

public class TokenReaderParams
{
    public readonly char[] StartTag;
    public readonly char[] EndTag;
    public readonly bool CanBeSpaceAfterStartTag;
    public readonly bool CanBeSpaceBeforeCloseTag;
    public readonly bool CanBeWithoutCloseTag;
    public readonly bool CanBeEmpty;
    public readonly bool CloseTagCanBeBetweenNumbers;

    public TokenReaderParams(string startTag, string endTag, bool closeTagCanBeBetweenNumbers = true, bool canBeSpaceAfterStartTag = true,
        bool canBeSpaceBeforeCloseTag = true, bool canBeWithoutCloseTag = false, bool canBeEmpty = false)
    {
        StartTag = startTag.ToCharArray();
        EndTag = endTag.ToCharArray();
        CloseTagCanBeBetweenNumbers = closeTagCanBeBetweenNumbers;
        CanBeEmpty = canBeEmpty;
        CanBeSpaceAfterStartTag = canBeSpaceAfterStartTag;
        CanBeSpaceBeforeCloseTag = canBeSpaceBeforeCloseTag;
        CanBeWithoutCloseTag = canBeWithoutCloseTag;
    }
}