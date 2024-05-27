using BenchmarkDotNet.Attributes;

namespace NameCaserBenchmark;

//| Method                       | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|----------------------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
//| CamelCaseOrig                | 31.94 ns | 0.543 ns | 0.453 ns |  1.00 |    0.00 | 0.0459 |     144 B |        1.00 |
//| CamelCaseDoWile              | 30.42 ns | 0.155 ns | 0.145 ns |  0.95 |    0.01 | 0.0459 |     144 B |        1.00 |
//| CamelCaseStringCreateDoWhile | 19.46 ns | 0.328 ns | 0.307 ns |  0.61 |    0.02 | 0.0229 |      72 B |        0.50 |
//| CamelCaseStringCreate        | 20.66 ns | 0.462 ns | 0.432 ns |  0.65 |    0.01 | 0.0229 |      72 B |        0.50 |
//| CamelCaseUnsafe              | 27.04 ns | 0.231 ns | 0.216 ns |  0.85 |    0.01 | 0.0229 |      72 B |        0.50 |

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
    public string CamelCaseDoWile()
    {
        var chars = _pascalCase.ToCharArray();

        int i = 0;
        do
        {
            chars[i] = char.ToLowerInvariant(chars[i]);
            i++;
        } while (i + 1 < chars.Length && !char.IsUpper(chars[i + 1]));

        return new string(chars);
    }

    [Benchmark]
    public string CamelCaseStringCreateDoWhile()
    {
        return string.Create(_pascalCase.Length, _pascalCase, (span, input) =>
        {
            var chars = input.AsSpan();
            chars.CopyTo(span);
            int i = 0;
            do
            {
                span[i] = char.ToLowerInvariant(chars[i]);
                i++;
            } while (i < chars.Length && char.IsUpper(chars[i]));
        });
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
