namespace NameCaser;

public static class ToTrainCaseExtension
{
    /// <summary>
    /// PacalCase to Train-Case, eg. IODriver to 'IO-Driver'
    /// </summary>
    public static string? ToTrainCase(this string pascalCase) =>
    pascalCase.ToCase((type, c) => type switch
    {
        Types.Break => '-',
        _ => c,
    });
}