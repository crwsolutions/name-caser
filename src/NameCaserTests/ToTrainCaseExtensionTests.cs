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
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToTrainCase();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToTrainCase_ReturnsEmpty_WhenInputIsEmpty()
    {
        // Arrange
        var input = "";

        // Act
        var result = input.ToTrainCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData("TrainCase", "Train-Case")]
    [InlineData("CaseWithNumberPostfix1", "Case-With-Number-Postfix1")]
    [InlineData("MixedCaseString", "Mixed-Case-String")]
    [InlineData("IODriver", "IO-Driver")]
    [InlineData("SomeIODriver", "Some-IO-Driver")]
    public void ToTrainCase_ConvertsToTrainCase(string input, string expected)
    {
        // Act
        var result = input.ToTrainCase();

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("UPPERCASE", "UPPERCASE")]
    [InlineData("123", "123")] // No change for numeric input
    public void ToTrainCase_NoChangeForNonPascalCase(string input, string expected)
    {
        // Act
        var result = input.ToTrainCase();

        // Assert
        Assert.Equal(expected, result);
    }
}

