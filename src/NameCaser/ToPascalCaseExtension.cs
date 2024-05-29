using static System.Globalization.UnicodeCategory;

namespace NameCaser;

public static class ToPascalCaseExtension
{

    /// <summary>
    /// Phrase to PascalCase, eg. '- Some UTP cable' to SomeUTPCable
    /// </summary>
    public static string ToPascalCase(this string phrase)
    {
        if (string.IsNullOrEmpty(phrase))
        {
            return phrase;
        }

        var chars = phrase.AsSpan();
        var length = 0;
        var isSeparated = true;
        UnicodeCategory c;
        Span<char> span = new char[chars.Length];
        var i = 0;
        do
        {
            c = char.GetUnicodeCategory(chars[i]);
            if (c is not LowercaseLetter && c is not DecimalDigitNumber)
            {
                isSeparated = true;
            }

            if (c is LowercaseLetter || c is UppercaseLetter || c is DecimalDigitNumber)
            {
                span[length] = isSeparated ? char.ToUpperInvariant(chars[i]) : char.ToLowerInvariant(chars[i]);
                isSeparated = false;
                length++;
            }
            i++;
        } while (i < chars.Length);

        return span.Slice(0, length).ToString();
    }
}
