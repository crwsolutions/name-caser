using BenchmarkDotNet.Attributes;
using NameCaser;
using System.Text;

namespace NameCaserBenchmark;

//| Method         | Mean     | Error   | StdDev  | Ratio | Gen0   | Allocated | Alloc Ratio |
//|--------------- |---------:|--------:|--------:|------:|-------:|----------:|------------:|
//| CamelCaseOrig  | 146.8 ns | 1.98 ns | 1.76 ns |  1.00 | 0.0918 |     288 B |        1.00 |
//| CamelCaseFinal | 113.4 ns | 1.49 ns | 1.40 ns |  0.77 | 0.0663 |     208 B |        0.72 |

[MemoryDiagnoser(true)]
public class BenchMarksTrainCasing
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public string CamelCaseOrig()
    {
        if (_pascalCase is null) return null;

        if (_pascalCase.Length == 0) return string.Empty;

        var builder = new StringBuilder();

        for (var i = 0; i < _pascalCase.Length; i++)
        {
            if (_pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(_pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToUpper(_pascalCase[i]));
            }
            else if (char.IsLower(_pascalCase[i]))
            {
                builder.Append(_pascalCase[i]);
            }
            else if (_pascalCase[i] == '-')
            {
                //Skip
            }
            else if (char.IsLower(_pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('-');
                builder.Append(_pascalCase[i]);
            }
            else if (i + 1 == _pascalCase.Length || char.IsUpper(_pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(_pascalCase[i]);
            }
            else // if current char is upper and next char is lower
            {
                builder.Append('-');

                builder.Append(_pascalCase[i]);
            }
        }
        return builder.ToString();
    }

    [Benchmark]
    public string CamelCaseFinal()
    {
        return _pascalCase.ToTrainCase()!;
    }
}
