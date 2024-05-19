using System;

namespace NameCaser;
public static class AnalyzeStringExtension
{
    public static (Types[] Bytes, int Breaks) Analyze(this ReadOnlySpan<char> chars)
    {
        var types = new Types[chars.Length];
        var breaks = 0;
        Types last = Types.Break;
        for (var i = 0; i < chars.Length; i++)
        {
            if (char.IsUpper(chars[i]))
            {
                types[i] = Types.Upper;
                if (last == Types.Lower)
                {
                    types[i] = Types.Break;
                    breaks++;
                    last = Types.Break;
                }
                else if (i > 0)
                {
                    last = Types.Upper;
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