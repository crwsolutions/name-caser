using NameCaser;

namespace Tests;

public class ToConstantCaseExtensionTests
{
    [Fact]
    public void ToConstantCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string input = null;

        // Act
        string result = input.ToConstantCase();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToConstantCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        string result = input.ToConstantCase();

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
    public void ToConstantCase_ConvertsToConstantCase(string input, string expected)
    {
        // Act
        string result = input.ToConstantCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("UPPERCASE", "UPPERCASE")]
    [InlineData("ALREADY_CONST_CASE", "ALREADY_CONST_CASE")]
    [InlineData("123", "123")] // No change for numeric input
    public void ToConstantCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        string result = input.ToConstantCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

