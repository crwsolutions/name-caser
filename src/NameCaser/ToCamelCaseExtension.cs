namespace NameCaser;

public static class ToCamelCaseExtension
{

    /// <summary>
    /// PacalCase to camelCase, eg. IODriver to ioDriver
    /// </summary>
    public static string ToCamelCase(this string pascalCase)
    {
        if (string.IsNullOrEmpty(pascalCase))
        {
            return pascalCase;
        }

        if (!char.IsUpper(pascalCase[0]))
        {
            return pascalCase;
        }

#if NETSTANDARD2_0
        var chars = pascalCase.ToCharArray();
        var i = 0;
        do
        {
            chars[i] = char.ToLowerInvariant(chars[i]);
            i++;
        } while (i < chars.Length && !(i + 1 < chars.Length && !char.IsUpper(chars[i + 1])));

        return new string(chars);
#else
        return string.Create(pascalCase.Length, pascalCase, (span, input) =>
        {
            var chars = input.AsSpan();
            chars.CopyTo(span);
            var i = 0;
            do
            {
                span[i] = char.ToLowerInvariant(chars[i]);
                i++;
            } while (i < chars.Length && !(i + 1 < chars.Length && !char.IsUpper(chars[i + 1])));
        });
#endif
    }
}
