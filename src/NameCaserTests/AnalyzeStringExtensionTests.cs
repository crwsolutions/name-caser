using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NameCaser.Utils;

namespace Tests;
public  class AnalyzeStringExtensionTests
{
    [Fact]
    public void AnalyzeString_OfCaseWithNumberPostfix1_GivesOneCapital()
    {
        // Arrange
        string input = "CaseWithNumberPostfix1";

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(3, Breaks);
        Assert.Equal(Types.Upper, Bytes[0]);
        Assert.Equal(Types.Lower, Bytes[1]);
        Assert.Equal(Types.Break, Bytes[4]);
        Assert.Equal(Types.Lower, Bytes[5]);
        Assert.Equal(Types.Break, Bytes[8]);
    }

    [Fact]
    public void AnalyzeString_OfSome_GivesOneCapital()
    {
        // Arrange
        string input = "Some";

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(0, Breaks);
        Assert.Equal(Types.Upper, Bytes[0]);
        Assert.Equal(Types.Lower, Bytes[1]);
    }

    [Fact]
    public void AnalyzeString_OfUTPCable_GivesTwoCapitals()
    {
        // Arrange
        string input = "UTPCable";

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(1, Breaks); 
        Assert.Equal(Types.Upper, Bytes[0]);
        Assert.Equal(Types.Upper, Bytes[1]);
        Assert.Equal(Types.Upper, Bytes[2]);
        Assert.Equal(Types.Break, Bytes[3]);
        Assert.Equal(Types.Lower, Bytes[4]);
    }

    [Fact]
    public void AnalyzeString_OfUTP_GivesTwoCapitals()
    {
        // Arrange
        string input = "UTP";

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(0, Breaks);
        Assert.Equal(Types.Upper, Bytes[0]);
        Assert.Equal(Types.Upper, Bytes[1]);
        Assert.Equal(Types.Upper, Bytes[2]);
    }

    [Fact]
    public void AnalyzeString_OfSomeUTPCable_GivesThreeCapitals()
    {
        // Arrange
        string input = "SomeUTPCable";

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(2, Breaks);
        Assert.Equal(Types.Upper, Bytes[0]);
        Assert.Equal(Types.Lower, Bytes[1]);
        Assert.Equal(Types.Break, Bytes[4]);
        Assert.Equal(Types.Upper, Bytes[5]);
        Assert.Equal(Types.Upper, Bytes[6]);
        Assert.Equal(Types.Break, Bytes[7]);
        Assert.Equal(Types.Lower, Bytes[8]);
    }

    [Fact]
    public void AnalyzeString_OfSomeUTP_GivesTwoCapitals()
    {
        // Arrange
        string input = "SomeUTP";

        // Act
        var (Bytes, Breaks) = input.AsSpan().Analyze();

        // Assert
        Assert.Equal(1, Breaks);
        Assert.Equal(Types.Upper, Bytes[0]);
        Assert.Equal(Types.Lower, Bytes[1]);
        Assert.Equal(Types.Break, Bytes[4]);
        Assert.Equal(Types.Upper, Bytes[5]);
        Assert.Equal(Types.Upper, Bytes[6]);
    }
}
