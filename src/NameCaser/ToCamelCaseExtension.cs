using System;

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
#else
        return string.Create(pascalCase.Length, pascalCase, (span, input) =>
        {
            var chars = input.AsSpan();
            chars.CopyTo(span);
            for (var i = 0; i < span.Length; i++)
            {
                if (i > 0 && (i + 1 < chars.Length) && !char.IsUpper(chars[i + 1]))
                {
                    break;
                }

                span[i] = char.ToLowerInvariant(chars[i]);
            }
        });
#endif
    }
}
