namespace NameCaser;

public static class ToKebabCaseExtension
{
    /// <summary>
    /// PacalCase to kebab-case, eg. IODriver to io-driver
    /// </summary>
    public static string? ToKebabCase(this string pascalCase) =>
        pascalCase.ToCase((type, c) => type switch
        {
            Types.Break => '-',
            Types.Upper => char.ToLower(c),
            _ => c,
        });
}