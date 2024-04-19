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
        string result = input.ToCamelCase();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToCamelCase_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        string? input = "";

        // Act
        string result = input.ToCamelCase();

        // Assert
        Assert.Equal("",result);
    }

    [Theory]
    [InlineData("PascalCase", "pascalCase")]
    [InlineData("CamelCase", "camelCase")]
    [InlineData("UPPERCASE", "uppercase")]
    [InlineData("MixedCaseString", "mixedCaseString")]
    [InlineData("AlreadyCamelCase", "alreadyCamelCase")]
    [InlineData("IODriver", "ioDriver")]
    public void ToCamelCase_ConvertsToCamelCase(string input, string expected)
    {
        // Act
        string result = input.ToCamelCase();

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
        string result = input.ToCamelCase();

        // Assert
        Assert.Equal(expected, result);
    }
}
