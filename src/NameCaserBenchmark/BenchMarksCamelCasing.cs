using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCaserBenchmark;

[MemoryDiagnoser(true)]
public class BenchMarksCamelCasing
{
    readonly string _pascalCase = "IODeviceSomeLongerString";

    [Benchmark]
    public string CamelCaseOrig()
    {
        char[] chars = _pascalCase.ToCharArray();

        for (int i = 0; i < chars.Length; i++)
        {
            bool hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }

            chars[i] = char.ToLower(chars[i], CultureInfo.InvariantCulture);
        }

        return new string(chars);
    }

    [Benchmark]
    public string CamelCaseSpan()
    {
        var chars = _pascalCase.AsSpan();
        int i = 1;
        for (; i < chars.Length; i++)
        {
            bool hasNext = (i + 1 < chars.Length);
            if (hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }
        }
        return string.Create(_pascalCase.Length, _pascalCase, (span, str) =>
        {
            //str.AsSpan().CopyTo(span);
            for (int j = 0; j < i; j++)
            {
                span[j] = char.ToUpper(span[j]);
            }
        });
    }

    [Benchmark]
    public unsafe string CamelCaseUnsafe()
    {
        var chars = _pascalCase.AsSpan();
        int i = 1;
        for (; i < chars.Length; i++)
        {
            bool hasNext = (i + 1 < chars.Length);
            if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
            {
                break;
            }
        }
        var newString = new string(chars);

        fixed (char* p = newString)
        {
            for (int j = 0; j < i; j++)
            {
                p[j] = char.ToUpper(p[j]);
            }
        }
        return newString;
    }
}
