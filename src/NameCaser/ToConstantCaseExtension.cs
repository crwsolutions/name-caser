using NameCaser.Utils;

namespace NameCaser;

public static class ToConstantCaseExtension
{
    /// <summary>
    /// PacalCase to CONSTANT_CASE, eg. IODriver to 'IO_DRIVER'
    /// </summary>
    public static string? ToConstantCase(this string pascalCase) =>
        pascalCase.ToCase((type, c) => type switch
        {
            Types.Break => '_',
            Types.Upper => c,
            _ => char.ToUpper(c),
        });
}