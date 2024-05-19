using NameCaser;

namespace Tests;

public class ToSnakeCaseExtensionTests
{
    [Fact]
    public void ToSnakeCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        string? result = input.ToSnakeCase();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToSnakeCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        string? result = input.ToSnakeCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("PascalCase", "pascal_case")]
    [InlineData("SnakeCase", "snake_case")]
    [InlineData("CaseWithNumberPostfix1", "case_with_number_postfix1")]
    [InlineData("UPPERCASE", "uppercase")]
    [InlineData("MixedCaseString", "mixed_case_string")]
    [InlineData("already_kebab_case", "already_kebab_case")]
    [InlineData("IODriver", "io_driver")]
    [InlineData("SomeUTP", "some_utp")]
    public void ToSnakeCase_ConvertsToSnakeCase(string input, string expected)
    {
        // Act
        string? result = input.ToSnakeCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("lowercase", "lowercase")] // No change for lowercase input
    [InlineData("123", "123")] // No change for numeric input
    public void ToSnakeCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        string? result = input.ToSnakeCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

