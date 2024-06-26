using NameCaser;

namespace Tests;

public class ToConstantCaseExtensionTests
{
    [Fact]
    public void ToConstantCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToConstantCase();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToConstantCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        var input = "";

        // Act
        var result = input.ToConstantCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("lowercase", "LOWERCASE")]
    [InlineData("ConstantCase", "CONSTANT_CASE")]
    [InlineData("CaseWithNumberPostfix1", "CASE_WITH_NUMBER_POSTFIX1")]
    [InlineData("MixedCaseString", "MIXED_CASE_STRING")]
    [InlineData("all_lower_case", "ALL_LOWER_CASE")]
    [InlineData("IODriver", "IO_DRIVER")]
    [InlineData("SomeIODriver", "SOME_IO_DRIVER")]
    [InlineData("SomeUTP", "SOME_UTP")]
    public void ToConstantCase_ConvertsToConstantCase(string input, string expected)
    {
        // Act
        var result = input.ToConstantCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("UPPERCASE", "UPPERCASE")]
    [InlineData("0123456789", "0123456789")] // No change for numeric input
    public void ToConstantCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        var result = input.ToConstantCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

