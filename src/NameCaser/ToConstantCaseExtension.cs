using System.Text;

namespace NameCaser
{
    public static class ToConstantCaseExtension
    {

        /// <summary>
        /// PacalCase to CONSTANT_CASE, eg. IODriver to 'IO_DRIVER'
        /// </summary>
        public static string ToConstantCase(this string pascalCase)
        {
            if (pascalCase is null) return null;

            if (pascalCase.Length == 0) return string.Empty;

            var builder = new StringBuilder();

            for (var i = 0; i < pascalCase.Length; i++)
            {
                if (char.IsNumber(pascalCase[i])) // if current char is number
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
    }
}