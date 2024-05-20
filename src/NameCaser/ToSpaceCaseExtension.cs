namespace NameCaser;

public static class ToSpaceCaseExtension
{

    /// <summary>
    /// PacalCase to Space case, eg. IODriver to 'IO driver'
    /// </summary>
    public static string? ToSpaceCase(this string pascalCase) =>
        pascalCase.ToCaseWithAbbreviations((type, c) => type switch
        {
            Types.Break => ' ',
            { } when type.Is(Types.Abbreviation) => c,
            { } when type.Is(Types.First) => c,
            { } when type.Is(Types.Upper) => char.ToLower(c),
            _ => c,
        });
}
