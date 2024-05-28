namespace NameCaser.Core;
internal static class ToCaseExtension
{
    internal static string ToCase(this string pascalCase, Func<Types, char, char> callBack)
    {
        if (string.IsNullOrEmpty(pascalCase))
        {
            return pascalCase;
        }

        var span = pascalCase.AsSpan();
        var (types, breaks) = span.Analyze();
        var bob = new CharBuilder(pascalCase.Length + breaks);
        for (var i = 0; i < types.Length; i++)
        {
            if (types[i] == Types.Break)
            {
                bob.Append(callBack(types[i], '*'));
                bob.Append(callBack(Types.Upper, span[i]));
                continue;
            }
            bob.Append(callBack(types[i], span[i]));
        }

        return bob.ToString();
    }

    internal static string ToCaseWithFlags(this string pascalCase, Func<Types, char, char> callBack)
    {
        if (string.IsNullOrEmpty(pascalCase))
        {
            return pascalCase;
        }

        var span = pascalCase.AsSpan();
        var (flags, breaks) = span.AnalyzeWithFlags();
        var bob = new CharBuilder(pascalCase.Length + breaks);
        for (var i = 0; i < flags.Length; i++)
        {
            if (flags[i].Has(Types.Break))
            {
                bob.Append(callBack(Types.Break, '*'));
            }
            bob.Append(callBack(flags[i], span[i]));
        }

        return bob.ToString();
    }
}
