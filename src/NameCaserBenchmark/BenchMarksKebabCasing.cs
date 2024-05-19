using BenchmarkDotNet.Attributes;
using NameCaser;
using NameCaserBenchmark;
using System.Text;

//| Method                       | Mean     | Error   | StdDev  | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|----------------------------- |---------:|--------:|--------:|------:|--------:|-------:|----------:|------------:|
//| KebabCaseOrig                | 139.9 ns | 1.69 ns | 1.58 ns |  1.00 |    0.00 | 0.0918 |     288 B |        1.00 |
//| KebabCaseFinal               | 149.1 ns | 1.41 ns | 1.10 ns |  1.06 |    0.02 | 0.0663 |     208 B |        0.72 |
//| KebabCaseWithAnalyzerAsBytes | 143.6 ns | 1.57 ns | 1.47 ns |  1.03 |    0.01 | 0.1018 |     320 B |        1.11 |

[MemoryDiagnoser(true)]
public class BenchMarksKebabCasing
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public string KebabCaseOrig()
    {
        return _pascalCase.ToLowerCaseWithSeperator('-')!;
    }

    [Benchmark]
    public string KebabCaseFinal()
    {
        return _pascalCase.ToKebabCase()!;
    }

    [Benchmark]
    public string KebabCaseWithAnalyzerAsBytes()
    {
        var span = _pascalCase.AsSpan();
        var (types, breaks) = span.Analyze();
        var bob = new CharBuilder(_pascalCase.Length + breaks);
        for (int i = 0; i < types.Length; i++)
        {
            if (types[i] == Types.Break)
            {
                bob.Append('_');
                bob.Append(char.ToLower(span[i]));
            }
            else if (types[i] == Types.Upper)
            {
                bob.Append(char.ToLower(span[i]));
            }
            else
            {
                bob.Append(span[i]);
            }
        }

        return bob.ToString();
    }

    //[Benchmark]
    public string KebabCaseUseReadOnlySpan()
    {
        return _pascalCase.UseReadOnlySpan();
    }

    //[Benchmark]
    public string KebabCaseUseCharBuilder()
    {
        return _pascalCase.UseCharBuilder();
    }

    //[Benchmark]
    public string KebabCaseUseBoth()
    {
        return _pascalCase.UseBoth();
    }

    //[Benchmark]
    public string KebabCaseUseBothWithStruct()
    {
        return _pascalCase.UseBoth();
    }
}

public static class KebabCaseOptimized
{
    public static string UseReadOnlySpan(this string input)
    {
        ReadOnlySpan<char> pascalCase = input;

        if (input is null) return null;

        if (pascalCase.Length == 0) return string.Empty;

        //var builder = new CharBuilder(pascalCase.Length*2);
        var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (char.IsLower(pascalCase[i])) // if current char is already lowercase
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (pascalCase[i] == '-')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else // if current char is upper and next char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
        }
        return builder.ToString();
    }

    public static string UseCharBuilder(this string pascalCase)
    {
        //ReadOnlySpan<char> pascalCase = input;

        if (pascalCase is null) return null;

        if (pascalCase.Length == 0) return string.Empty;

        var builder = new CharBuilder(pascalCase.Length * 2);
        //var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (char.IsLower(pascalCase[i])) // if current char is already lowercase
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (pascalCase[i] == '-')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else // if current char is upper and next char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
        }
        return builder.ToString();
    }

    public static string UseBoth(this string input)
    {
        ReadOnlySpan<char> pascalCase = input;

        if (input is null) return null;

        if (pascalCase.Length == 0) return string.Empty;

        var builder = new CharBuilder(pascalCase.Length * 2);
        //var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (char.IsLower(pascalCase[i])) // if current char is already lowercase
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (pascalCase[i] == '-')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else // if current char is upper and next char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
        }
        return builder.ToString();
    }

    public static string UseBothWithStruct(this string input)
    {
        ReadOnlySpan<char> pascalCase = input;

        if (input is null) return null;

        if (pascalCase.Length == 0) return string.Empty;

        var builder = new StructBuilder(pascalCase.Length * 2);
        //var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (char.IsLower(pascalCase[i])) // if current char is already lowercase
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (pascalCase[i] == '-')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else // if current char is upper and next char is lower
            {
                builder.Append('-');
                builder.Append(char.ToLower(pascalCase[i]));
            }
        }
        return builder.ToString();
    }
    internal static bool IsNumber(this char c) => c >= '0' && c <= '9';
}
