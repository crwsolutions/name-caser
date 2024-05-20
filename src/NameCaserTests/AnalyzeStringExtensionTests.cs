using NameCaser.Utils;
using System.Text;
using Xunit.Abstractions;

namespace Tests;
public class AnalyzeStringExtensionTests(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Theory]
    [InlineData("CaseWithNumberPostfix1", "1000200020000020000000", 3)]
    [InlineData("Some", "1000", 0)]
    [InlineData("PoCo", "1020", 1)]
    [InlineData("", "", 0)]
    [InlineData("UTP", "111", 0)]
    [InlineData("UTPCable", "11120000", 1)]
    [InlineData("SomeUTPCable", "100021120000", 2)]
    [InlineData("SomeUTP", "1000211", 1)]
    public void Analyze_Input_ShouldGiveExpected(string input, string expected, int breaks)
    {
        _output.WriteLine(input);

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(breaks, Breaks);
        Assert.Equal(expected, ToFlagsString(Bytes));
    }

    [Theory]
    [InlineData("CaseWithNumberPostfix1", "9000300030000030000000", 3)]
    [InlineData("Some", "9000", 0)]
    [InlineData("PoCo", "9030", 1)]
    [InlineData("", "", 0)]
    [InlineData("UTP", "1355", 0)]
    [InlineData("UTPCable", "135530000", 1)]
    [InlineData("SomeUTPCable", "900075530000", 2)]
    [InlineData("SomeUTP", "9000755", 1)]
    public void AnalyzeWithAbbreviations_Input_ShouldGiveExpected(string input, string expected, int breaks)
    {
        _output.WriteLine(input);

        // Act
        var (Bytes, Breaks) = input.AsSpan().AnalyzeWithFlags();

        // Assert
        Assert.Equal(breaks, Breaks);
        Assert.Equal(expected, ToFlagsString(Bytes));
    }

    private static string ToFlagsString(Types[] Bytes)
    {
        var bob = new StringBuilder();
        for (var i = 0; i < Bytes.Length; i++)
        {
            bob.Append((int)Bytes[i]);
        }

        var s = bob.ToString();
        return s;
    }
}
