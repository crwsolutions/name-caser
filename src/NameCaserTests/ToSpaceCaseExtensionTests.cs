using NameCaser;

namespace Tests;

public class ToSpaceCaseExtensionTests
{
    [Fact]
    public void ToSpaceCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        string? result = input.ToSpaceCase();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToSpaceCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        string? result = input.ToSpaceCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("SpaceCase", "Space case")]
    [InlineData("CaseWithNumberPostfix1", "Case with number postfix1")]
    [InlineData("MixedCaseString", "Mixed case string")]
    [InlineData("IODriver", "IO driver")]
    [InlineData("UTPCable", "UTP cable")]
    [InlineData("SomeUTPCable", "Some UTP cable")]
    [InlineData("SomeUTP", "Some UTP")]
    public void ToSpaceCase_ConvertsToSpaceCase(string input, string expected)
    {
        // Act
        string? result = input.ToSpaceCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("lowercase", "lowercase")] // No change for lowercase input
    [InlineData("UPPERCASE", "UPPERCASE")]
    [InlineData("123", "123")] // No change for numeric input
    public void ToSpaceCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        string? result = input.ToSpaceCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

