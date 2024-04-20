using System.Text;

namespace NameCaser;

internal static class ToLowerCaseWithSeperatorExtension
{
    public static string? ToLowerCaseWithSeperator(this string pascalCase, char seperator)
    {
        if (pascalCase is null) return null;

        if (pascalCase.Length == 0) return string.Empty;

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
            else if (pascalCase[i] == seperator)
            {
                builder.Append(pascalCase[i]);
            }
            else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
            {
                builder.Append(seperator);
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else if (i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1])) // if current char is upper and next char doesn't exist or is upper
            {
                builder.Append(char.ToLower(pascalCase[i]));
            }
            else // if current char is upper and next char is lower
            {
                builder.Append(seperator);
                builder.Append(char.ToLower(pascalCase[i]));
            }
        }
        return builder.ToString();
    }
}
