using BenchmarkDotNet.Attributes;
using NameCaser;
using System.Text;

namespace NameCaserBenchmark;

//Before:

//| Method         | Mean     | Error    | StdDev   | Ratio | Gen0   | Allocated | Alloc Ratio |
//|--------------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
//| CamelCaseOrig  | 94.72 ns | 1.631 ns | 1.526 ns |  1.00 | 0.0343 |     288 B |        1.00 |
//| CamelCaseFinal | 72.78 ns | 0.937 ns | 0.783 ns |  0.77 | 0.0248 |     208 B |        0.72 |

//Inline:

//| Method         | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|--------------- |---------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
//| TrainCaseOrig  | 93.56 ns | 1.880 ns | 1.758 ns |  1.00 |    0.00 | 0.0343 |     288 B |        1.00 |
//| TrainCaseFinal | 79.89 ns | 0.989 ns | 0.925 ns |  0.85 |    0.02 | 0.0248 |     208 B |        0.72 |

//Stackalloc:

//| Method         | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
//|--------------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
//| TrainCaseOrig  |  93.10 ns | 1.259 ns | 1.178 ns |  1.00 |    0.00 | 0.0343 |     288 B |        1.00 |
//| TrainCaseFinal | 115.22 ns | 1.401 ns | 1.170 ns |  1.24 |    0.03 | 0.0191 |     160 B |        0.56 |

//String.Create:

//| Method         | Mean     | Error    | StdDev   | Ratio | Gen0   | Allocated | Alloc Ratio |
//|--------------- |---------:|---------:|---------:|------:|-------:|----------:|------------:|
//| TrainCaseOrig  | 98.91 ns | 0.961 ns | 0.899 ns |  1.00 | 0.0343 |     288 B |        1.00 |
//| TrainCaseFinal | 75.61 ns | 1.053 ns | 0.985 ns |  0.76 | 0.0267 |     224 B |        0.78 |

[MemoryDiagnoser(true)]
public class BenchMarksTrainCasing
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public string? TrainCaseOrig()
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
    public string TrainCaseFinal()
    {
        return _pascalCase.ToTrainCase()!;
    }
}
