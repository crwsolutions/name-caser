using NameCaser;

namespace Tests;

public class ToKebabCaseExtensionTests
{
    [Fact]
    public void ToKebabCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string input = null;

        // Act
        string result = input.ToKebabCase();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToKebabCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        string result = input.ToKebabCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("PascalCase", "pascal-case")]
    [InlineData("CamelCase", "camel-case")]
    [InlineData("CaseWithNumberPostfix1", "case-with-number-postfix1")]
    [InlineData("UPPERCASE", "uppercase")]
    [InlineData("MixedCaseString", "mixed-case-string")]
    [InlineData("already-kebab-case", "already-kebab-case")]
    [InlineData("IODriver", "io-driver")]
    [InlineData("SomeIODriver", "some-io-driver")]
    public void ToKebabCase_ConvertsToKebabCase(string input, string expected)
    {
        // Act
        string result = input.ToKebabCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("lowercase", "lowercase")] // No change for lowercase input
    [InlineData("123", "123")] // No change for numeric input
    public void ToKebabCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        string result = input.ToKebabCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

