using System;

namespace NameCaser;
public static class AnalyzeStringExtension
{
    public static (byte[] Bytes, int Breaks) Analyze(this string value)
    {
        var bytes = new byte[value.Length];
        var breaks = 0;
        var chars = value.AsSpan();
        byte last = 2;
        for (var i = 0; i < chars.Length; i++)
        {
            if (char.IsUpper(chars[i]))
            {
                bytes[i] = 1;
                if (last == 0)
                {
                    bytes[i]++;
                    breaks++;
                    last = 2;
                }
                else if (i > 0)
                {
                    last = 1;
                }
            }
            else
            {
                if (last == 1)
                {
                    bytes[i - 1]++;
                    breaks++;
                }

                last = 0;
            }
        }

        return (bytes, breaks);
    }
}