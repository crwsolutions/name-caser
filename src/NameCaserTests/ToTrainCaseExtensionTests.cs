using NameCaser;

namespace Tests;

public class ToTrainCaseExtensionTests
{
    [Fact]
    public void ToTrainCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
        string result = input.ToTrainCase();

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToTrainCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        string input = "";

        // Act
        string result = input.ToTrainCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("lowercase", "Lowercase")]
    [InlineData("TrainCase", "Train-Case")]
    [InlineData("CaseWithNumberPostfix1", "Case-With-Number-Postfix1")]
    [InlineData("MixedCaseString", "Mixed-Case-String")]
    [InlineData("IODriver", "IO-Driver")]
    [InlineData("SomeIODriver", "Some-IO-Driver")]
    public void ToTrainCase_ConvertsToTrainCase(string input, string expected)
    {
        // Act
        string result = input.ToTrainCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("UPPERCASE", "UPPERCASE")]
    [InlineData("Already-Train-Case", "Already-Train-Case")]
    [InlineData("123", "123")] // No change for numeric input
    public void ToTrainCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        string result = input.ToTrainCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

