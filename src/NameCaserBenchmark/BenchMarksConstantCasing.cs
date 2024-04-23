using BenchmarkDotNet.Attributes;
using NameCaserBenchmark;
using System.Text;

[MemoryDiagnoser(true)]
public class BenchMarksConstantCasing
{
    readonly string pascalCase = "IODeviceSomeLongerString";

    [Benchmark(Baseline = true)]
    public string ConstantCaseOrig()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (pascalCase[i] == '_')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i]))
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(pascalCase[i]);
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(char.ToUpper(pascalCase[i]));
            }
        }
        return builder.ToString();
    }

    [Benchmark]
    public string ConstantCaseShuffleIfs()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (char.IsLower(pascalCase[i]))
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i] == '_')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(pascalCase[i]);
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(char.ToUpper(pascalCase[i]));
            }
        }
        return builder.ToString();
    }

    //[Benchmark]
    public string ConstantCaseRemoveFirstIf()
    {
        var builder = new StringBuilder();
        builder.Append(char.ToUpper(pascalCase[0]));
        for (var i = 1; i < pascalCase.Length; i++)
        {
            if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i] == '_')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i]))
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(pascalCase[i]);
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(char.ToUpper(pascalCase[i]));
            }
        }
        return builder.ToString();
    }

    //[Benchmark]
    public string ConstantCasePutCharInVar()
    {
        var builder = new StringBuilder();
        var lastIsLower = true;
        for (var i = 0; i < pascalCase.Length; i++)
        {
            var c = pascalCase[i];
            if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToUpper(c));
            }
            else if (c.IsNumber()) // if current char is number
            {
                builder.Append(c);
                lastIsLower = false;
            }
            else if (c == '_')
            {
                builder.Append(c);
                lastIsLower = false;
            }
            else if (char.IsLower(c))
            {
                builder.Append(char.ToUpper(c));
                lastIsLower = true;
            }
            else if (lastIsLower) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(char.ToUpper(c));
                lastIsLower = false;
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(c);
                lastIsLower = false;
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(char.ToUpper(c));
                lastIsLower = false;
            }
        }
        return builder.ToString();
    }

    //[Benchmark]
    public string ConstantCaseSaveLastCase()
    {
        var builder = new StringBuilder();
        var lastIsLower = true;
        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
                lastIsLower = false;
            }
            else if (pascalCase[i] == '_')
            {
                builder.Append(pascalCase[i]);
                lastIsLower = false;
            }
            else if (char.IsLower(pascalCase[i]))
            {
                builder.Append(char.ToUpper(pascalCase[i]));
                lastIsLower = true;
            }
            else if (lastIsLower) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(char.ToUpper(pascalCase[i]));
                lastIsLower = false;
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(pascalCase[i]);
                lastIsLower = false;
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(char.ToUpper(pascalCase[i]));
                lastIsLower = false;
            }
        }
        return builder.ToString();
    }

    //[Benchmark]
    public string ConstantCaseUpperCaseInTheEnd()
    {
        var builder = new StringBuilder();

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(char.ToUpper(pascalCase[i]));
            }
            else if (pascalCase[i] == '_')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i]))
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(pascalCase[i]);
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(pascalCase[i]);
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(pascalCase[i]);
            }
        }
        return builder.ToString().ToUpperInvariant();
    }

    //[Benchmark]
    public string ConstantCaseUpperCaseInTheEndCharbuilder()
    {
        var builder = new CharBuilder(pascalCase.Length *2);

        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (pascalCase[i].IsNumber()) // if current char is number
            {
                builder.Append(pascalCase[i]);
            }
            else if (i == 0) // if current char is the first char
            {
                builder.Append(pascalCase[i]);
            }
            else if (pascalCase[i] == '_')
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i]))
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append('_');
                builder.Append(pascalCase[i]);
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(pascalCase[i]);
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    builder.Append('_');
                }

                builder.Append(pascalCase[i]);
            }
        }
        return builder.ToString().ToUpperInvariant();
    }

    //[Benchmark]
    public string ConstantCaseUpperCaseInTheEndSpanAlloc()
    {
        Span<char> chars = stackalloc char[pascalCase.Length * 2];
        int charIndex = 0;
        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (pascalCase[i].IsNumber()) // if current char is number
            {
                chars[charIndex++] = pascalCase[i];
            }
            else if (i == 0) // if current char is the first char
            {
                chars[charIndex++] = pascalCase[i];
            }
            else if (pascalCase[i] == '_')
            {
                chars[charIndex++] = pascalCase[i];
            }
            else if (char.IsLower(pascalCase[i]))
            {
                chars[charIndex++] = pascalCase[i];
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                chars[charIndex++] = '_';
                chars[charIndex++] = pascalCase[i];
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                chars[charIndex++] = pascalCase[i];
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    chars[charIndex++] = '_';
                }

                chars[charIndex++] = pascalCase[i];
            }
        }
        return (new string(chars[0..(charIndex-1)])).ToUpperInvariant();
    }

    [Benchmark]
    public string ConstantCaseAll()
    {
        Span<char> chars = stackalloc char[pascalCase.Length * 2];
        int charIndex = 0;
        var lastIsLower = false;
        chars[charIndex++] = pascalCase[1];
        for (var i = 1; i < pascalCase.Length; i++)
        {
            var c = pascalCase[i];
            if (c.IsNumber()) // if current char is number
            {
                chars[charIndex++] = c;
                lastIsLower = true;
            }
            else if (c == '_')
            {
                chars[charIndex++] = c;
                lastIsLower = true;
            }
            else if (char.IsLower(c))
            {
                chars[charIndex++] = c;
                lastIsLower = true;
            }
            else if (lastIsLower) // if current char is upper and previous char is lower
            {
                chars[charIndex++] = '_';
                chars[charIndex++] = c;
                lastIsLower = false;
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                chars[charIndex++] = c;
                lastIsLower = false;
            }
            else // if current char is upper and next char is lower
            {
                if (pascalCase[i + 1] != '_')
                {
                    chars[charIndex++] = '_';
                }

                chars[charIndex++] = c;
                lastIsLower = false;
            }
        }
        return (new string(chars[0..(charIndex - 1)])).ToUpperInvariant();
    }
}
