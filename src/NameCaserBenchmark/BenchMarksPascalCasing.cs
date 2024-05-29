using BenchmarkDotNet.Attributes;
using NameCaser;
using System.Globalization;
using static System.Globalization.UnicodeCategory;

namespace NameCaserBenchmark;

//| Method         | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|--------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
//| CamelCaseOrig  | 154.2 ns | 1.95 ns | 1.73 ns |  1.00 |    0.00 | 0.0918 |     288 B |        1.00 |
//| CamelCaseFinal | 158.7 ns | 2.67 ns | 3.27 ns |  1.04 |    0.02 | 0.0663 |     208 B |        0.72 |

[MemoryDiagnoser(true)]
public class BenchMarksPascalCasing
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public string? CamelCaseOrig()
    {
        if (string.IsNullOrEmpty(_pascalCase))
        {
            return _pascalCase;
        }

        var chars = _pascalCase.AsSpan();
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

    [Benchmark]
    public string? CamelCaseStackAlloc()
    {
        if (string.IsNullOrEmpty(_pascalCase))
        {
            return _pascalCase;
        }

        var chars = _pascalCase.AsSpan();
        var length = 0;
        var isSeparated = true;
        UnicodeCategory c;
        Span<char> span = stackalloc char[chars.Length];
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

    private static bool IsPartOfAbbreviation(string pascalCase, int i) => i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1]);
}
