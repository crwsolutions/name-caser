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
                    types[i] = Types.Upper;
                    if (i > 0)
                    {
                        last = Types.Upper;
                    }
                }
            }
            else
            {
                if (last == Types.Upper)
                {
                    types[i - 1] = Types.Break;
                    breaks++;
                }

                last = Types.Lower;
            }
        }

        return (types, breaks);
    }
}
