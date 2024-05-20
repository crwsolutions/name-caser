using NameCaser.Utils;
using System.Text;
using Xunit.Abstractions;

namespace NameCaserTests;
public class AnalyzeStringExtensionTests
{
    private readonly ITestOutputHelper _output;

    public AnalyzeStringExtensionTests(ITestOutputHelper output)
    {
        this._output = output;
    }

    [Theory]
    [InlineData("CaseWithNumberPostfix1", "1000300030000030000000", 3)]
    [InlineData("Some", "1000", 0)]
    [InlineData("PoCo", "1030", 1)]
    [InlineData("", "", 0)]
    [InlineData("UTP", "111", 0)]
    [InlineData("UTPCable", "11130000", 1)]
    [InlineData("SomeUTPCable", "100031130000", 2)]
    [InlineData("SomeUTP", "1000311", 1)]
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
        var (Bytes, Breaks) = input.AsSpan().AnalyzeWithAbbreviations();

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
