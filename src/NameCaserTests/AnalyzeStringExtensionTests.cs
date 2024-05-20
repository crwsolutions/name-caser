using NameCaser.Utils;
using System.Text;

namespace NameCaserTests;
public class AnalyzeStringExtensionTests
{
    [Theory]
    [InlineData("CaseWithNumberPostfix1", "1000300030000030000000", 3)]
    [InlineData("Some", "1000", 0)]
    [InlineData("PoCo", "1030", 1)]
    [InlineData("", "", 0)]
    [InlineData("UTP", "111", 0)]
    [InlineData("UTPCable", "11130000", 1)]
    [InlineData("SomeUTPCable", "100031130000", 2)]
    [InlineData("SomeUTP", "1000311", 1)]
    public void AnalyzeString_Input_ShouldGiveExpected(string input, string expected, int breaks)
    {
        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

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
