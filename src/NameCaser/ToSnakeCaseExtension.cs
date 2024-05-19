namespace NameCaser;

public static class ToSnakeCaseExtension
{
    /// <summary>
    /// PacalCase to snake_case, eg. IODriver to io_driver
    /// </summary>
    public static string? ToSnakeCase(this string pascalCase) =>
        pascalCase.ToCase((type, c) => type switch
        {
            Types.Break => '_',
            Types.Upper => char.ToLower(c),
            _ => c,
        });
}