namespace NameCaser;

public static class ToSpaceCaseExtension
{

    /// <summary>
    /// PacalCase to Space case, eg. IODriver to 'IO driver'
    /// </summary>
    public static string? ToSpaceCase(this string pascalCase) =>
        pascalCase.ToCaseWithFlags((flags, c) => flags switch
        {
            Types.Break => ' ',
            { } when flags.Has(Types.Abbreviation) => c,
            { } when flags.Has(Types.First) => c,
            { } when flags.Has(Types.Upper) => char.ToLower(c),
            _ => c,
        });
}
