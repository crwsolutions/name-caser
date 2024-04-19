using System.Globalization;

namespace NameCaser
{
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

            char[] chars = pascalCase.ToCharArray();

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
    }
}
