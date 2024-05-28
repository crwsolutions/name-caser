using NameCaser.Core;
using System.Buffers;

namespace NameCaserBenchmark;
public struct StringAnalyzer : IDisposable
{
    private Types[] _types;

    internal (Types[] Bytes, int Breaks) Analyze(ReadOnlySpan<char> chars)
    {
        _types = ArrayPool<Types>.Shared.Rent(chars.Length);
        var breaks = 0;
        var last = Types.Break;
        for (var i = 0; i < chars.Length; i++)
        {
            if (char.IsUpper(chars[i]))
            {
                if (last == Types.Lower)
                {
                    _types[i] = Types.Break;
                    breaks++;
                    last = Types.Break;
                }
                else
                {
                    _types[i] = Types.Upper;
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
                    _types[i - 1] = Types.Break;
                    breaks++;
                }

                last = Types.Lower;
            }
        }

        return (_types, breaks);
    }

    public void Dispose()
    {
        if (_types != null)
        {
            ArrayPool<Types>.Shared.Return(_types);
        }
    }
}
