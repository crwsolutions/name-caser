using BenchmarkDotNet.Attributes;

namespace NameCaserBenchmark;

//| Method                | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|---------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
//| CamelCaseOrig         | 21.80 ns | 0.474 ns | 0.487 ns |  1.00 |    0.00 | 0.0172 |     144 B |        1.00 |
//| CamelCaseStringCreate | 12.37 ns | 0.094 ns | 0.084 ns |  0.57 |    0.01 | 0.0086 |      72 B |        0.50 |
//| CamelCaseUnsafe       | 16.25 ns | 0.088 ns | 0.078 ns |  0.74 |    0.02 | 0.0086 |      72 B |        0.50 |

[MemoryDiagnoser(true)]
public class BenchMarksCamelCasing
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public string CamelCaseOrig()
    {
        var chars = _pascalCase.ToCharArray();

        for (var i = 0; i < chars.Length; i++)
        {
            var hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }

            chars[i] = char.ToLowerInvariant(chars[i]);
        }

        return new string(chars);
    }

    [Benchmark]
    public string CamelCaseStringCreate()
    {
        return string.Create(_pascalCase.Length, _pascalCase, (span, input) =>
        {
            var chars = input.AsSpan();
            chars.CopyTo(span);
            for (var i = 0; i < span.Length; i++)
            {
                var hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                span[i] = char.ToLowerInvariant(chars[i]);
            }
        });
    }

    [Benchmark]
    public unsafe string CamelCaseUnsafe()
    {
        var chars = _pascalCase.AsSpan();
        var i = 1;
        for (; i < chars.Length; i++)
        {
            var hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }
        }
        var newString = new string(chars);

        fixed (char* p = newString)
        {
            for (var j = 0; j < i; j++)
            {
                p[j] = char.ToUpper(p[j]);
            }
        }
        return newString;
    }
}
