using System;

namespace NameCaser.Utils;
internal static class AnalyzeStringExtension
{
    internal static (Types[] Bytes, int Breaks) Analyze(this ReadOnlySpan<char> chars)
    {
        var types = new Types[chars.Length];
        var breaks = 0;
        var last = Types.Break;
        for (var i = 0; i < chars.Length; i++)
        {
            if (char.IsUpper(chars[i]))
            {
                if (last == Types.Lower)
                {
                    types[i] = Types.Break;
                    breaks++;
                    last = Types.Break;
                }
                else
                {
                    if (i > 0)
                    {
                        last = Types.Upper;
                    }
                }
                types[i] |= Types.Upper;
            }
            else
            {
                if (last == Types.Upper)
                {
                    types[i - 1] |= Types.Break;
                    breaks++;
                }

                last = Types.Lower;
            }
        }
        return (types, breaks);
    }

    internal static (Types[] Bytes, int Breaks) AnalyzeWithAbbreviations(this ReadOnlySpan<char> chars)
    {
        var types = new Types[chars.Length];
        var breaks = 0;
        if (chars.Length == 0)
        {
            return (types, breaks);
        }
        var last = Types.Break;
        types[0] = Types.First;
        for (var i = 0; i < chars.Length; i++)
        {
            if (char.IsUpper(chars[i]))
            {
                if (last == Types.Lower)
                {
                    types[i] = Types.Break;
                    breaks++;
                    last = Types.Break;
                }
                else
                {
                    if (i > 0)
                    {
                        types[i] = Types.Abbreviation;
                        types[i - 1] |= Types.Abbreviation;
                        last = Types.Upper;
                    }
                }
                types[i] |= Types.Upper;
            }
            else
            {
                if (last == Types.Upper)
                {
                    types[i - 1] |= Types.Break;
                    types[i - 1] &= ~Types.Abbreviation;
                    breaks++;
                }

                last = Types.Lower;
            }
        }

        return (types, breaks);
    }
}
