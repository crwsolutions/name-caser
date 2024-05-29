using NameCaser;

namespace Tests;

public class ToPascalCaseExtensionTests
{
    [Fact]
    public void ToPascalCase_ReturnsNull_WhenInputIsNull()
    {
        // Arrange
        string? input = null;

        // Act
#pragma warning disable CS8604 // Possible null reference argument.
        var result = input.ToPascalCase();
#pragma warning restore CS8604 // Possible null reference argument.

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ToPascalCase_ReturnsEmptyString_WhenInputIsEmpty()
    {
        // Arrange
        var input = "";

        // Act
        var result = input.ToPascalCase();

        // Assert
        Assert.Equal("", result);
    }

    [Theory]
    [InlineData(" - some Example + Name   666", "SomeExampleName666")]
    [InlineData(" - some UTP - _ Cable", "SomeUTPCable")]
    [InlineData(" -  ", "")]
    [InlineData("IO Device", "IODevice")]
    [InlineData("Some UTP Cable", "SomeUTPCable")]
    [InlineData("Some UTP", "SomeUTP")]
    [InlineData("Some UTP  ", "SomeUTP")]
    [InlineData("u", "U")]
    [InlineData("U", "U")]
    public void ToPascalCase_ConvertsToPascalCase(string input, string expected)
    {
        // Act
        var result = input.ToPascalCase();

        // Assert
        Assert.Equal(expected, result);
    }
}
