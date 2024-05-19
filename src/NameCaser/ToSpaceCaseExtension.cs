namespace NameCaser;

public static class ToSpaceCaseExtension
{

    /// <summary>
    /// PacalCase to Space Case, eg. IODriver to 'IO driver'
    /// </summary>
    public static string? ToSpaceCase(this string pascalCase)
    {
        if (string.IsNullOrEmpty(pascalCase)) return pascalCase;

        var builder = new StringBuilder();
        var last = Types.Break;
        for (var i = 0; i < pascalCase.Length; i++)
        {
            if (char.IsUpper(pascalCase[i]))
            {
                if (i == 0)
                {
                    builder.Append(pascalCase[i]);
                    last = Types.Upper;
                    continue;
                }

                if (last == Types.Lower) // if current char is upper and previous char is lower
                {
                    builder.Append(' ');
                    if (IsPartOfAbbreviation(pascalCase, i))
                    {
                        builder.Append(pascalCase[i]);
                    }
                    else
                    {
                        builder.Append(char.ToLower(pascalCase[i]));
                    }
                }
                else if (IsPartOfAbbreviation(pascalCase, i)) // if current char is upper and next char doesn't exist or is upper
                {
                    builder.Append(pascalCase[i]);
                }
                else // if current char is upper and next char is lower
                {
                    builder.Append(' ');
                    builder.Append(char.ToLower(pascalCase[i]));
                }
                last = Types.Upper;
            }
            else
            { 
                builder.Append(pascalCase[i]);
                last = Types.Lower;
            }

        }
        return builder.ToString();
    }

    private static bool IsPartOfAbbreviation(string pascalCase, int i) => i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1]);
}