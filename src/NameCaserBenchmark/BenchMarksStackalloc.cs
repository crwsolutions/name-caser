using BenchmarkDotNet.Attributes;

namespace NameCaserBenchmark;

//| Method            | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|------------------ |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
//| AnalyzeBase       | 30.06 ns | 0.608 ns | 0.747 ns |  1.00 |    0.00 | 0.0057 |      48 B |        1.00 |
//| AnalyzeStackalloc | 33.11 ns | 0.298 ns | 0.279 ns |  1.10 |    0.03 |      - |         - |        0.00 |

[MemoryDiagnoser(true)]
public class BenchMarksStackalloc
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public Types AnalyzeBase()
    {
        return AnalyzeBaseLine(_pascalCase);
    }

    [Benchmark]
    public Types AnalyzeStackalloc()
    {
        return AnalyzeStackalloc(_pascalCase);
    }

    [Flags]
    public enum Types : byte
    {
        Lower = 0,              // 0000 0000
        Upper = 1 << 0,         // 0000 0001
        Break = 1 << 1,         // 0000 0010
        Abbreviation = 1 << 2,  // 0000 0100
        First = 1 << 3          // 0000 1000
    }

    public static Types AnalyzeBaseLine(ReadOnlySpan<char> chars)
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
                    last = Types.Upper;
                }
            }
            else
            {
                if (last == Types.Upper && i > 1)
                {
                    types[i - 1] = Types.Break;
                    breaks++;
                }

                last = Types.Lower;
            }
        }

        return last;
    }

    public static Types AnalyzeStackalloc(ReadOnlySpan<char> chars)
    {
        Span<Types> types = stackalloc Types[chars.Length];
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
                    last = Types.Upper;
                }
            }
            else
            {
                if (last == Types.Upper && i > 1)
                {
                    types[i - 1] = Types.Break;
                    breaks++;
                }

                last = Types.Lower;
            }
        }

        return last;
    }
}
