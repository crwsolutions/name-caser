using NameCaser;

namespace Tests;

public class ToCamelCaseExtensionTests
{
    [Fact]
    public void ToCamelCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToCamelCase();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToCamelCase_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = "";

        // Act
        var result = input.ToCamelCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("PascalCase", "pascalCase")]
    [InlineData("CamelCase", "camelCase")]
    [InlineData("UPPERCASE", "uppercase")]
    [InlineData("MixedCaseString", "mixedCaseString")]
    [InlineData("AlreadyCamelCase", "alreadyCamelCase")]
    [InlineData("IODriver", "ioDriver")]
    [InlineData("SomeIODriver", "someIODriver")]
    public void ToCamelCase_ConvertsToCamelCase(string input, string expected)
    {
        // Act
        var result = input.ToCamelCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("lowercase", "lowercase")] // No change for lowercase input
    [InlineData("123", "123")] // No change for numeric input
    [InlineData("", "")] // Empty string remains empty
    public void ToCamelCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        var result = input.ToCamelCase();

        // Assert
        Assert.Equal(expected, result);
    }
}
