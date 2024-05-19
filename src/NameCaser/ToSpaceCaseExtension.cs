using System.Text;
using NameCaser.Utils;

namespace NameCaser
{
    public static class ToSpaceCaseExtension
    {

        /// <summary>
        /// PacalCase to Space Case, eg. IODriver to 'IO driver'
        /// </summary>
        public static string? ToSpaceCase(this string pascalCase)
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
                    builder.Append(pascalCase[i]);
                }
                else if (char.IsLower(pascalCase[i - 1])) // if current char is upper and previous char is lower
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
            }
            return builder.ToString();
        }

        private static bool IsPartOfAbbreviation(string pascalCase, int i) => i + 1 == pascalCase.Length || char.IsUpper(pascalCase[i + 1]);
    }
}