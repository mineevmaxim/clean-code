using System.Text;

namespace Markdown;

public class Md
{
    /*
     * Принимает в качестве аргумента текст в markdown-подобной разметке, и возвращает строку с html-кодом
     */
    public string Render(string markdown)
    {
        return "";
    }

    public static string ConvertToHtml(Token token)
    {
        switch (token)
        {
            case StrongToken tokenStrong:
                if (tokenStrong.ChildTokens is null) return $"<strong>{tokenStrong.Value}</strong>";
                var sb = new StringBuilder();

                for (var i = 0; i < tokenStrong.Value.Length; i++)
                {
                    foreach (var child in tokenStrong.ChildTokens.OrderBy(t => t.Position))
                    {
                        if (child.Position != i) continue;
                        sb.Append(ConvertToHtml(child));
                        i = child.GetIndexNextToToken();
                        break;
                    }

                    sb.Append(tokenStrong.Value[i]);
                }

                return $"<strong>{sb}</strong>";
            case ItalicToken tokenItalic:
                return $"<em>{tokenItalic.Value}</em>";
            case HeaderToken tokenHeader:
                return $"<h{tokenHeader.TitleLevel}>{tokenHeader.Value}</h{tokenHeader.TitleLevel}>";
            default:
                return "";
        }
    }
}