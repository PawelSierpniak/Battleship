using BattleshipGame.Domain.GameBoard;
using FluentAssertions;

namespace BattleshipGame.Domain.Test.GameBoard;

public class CoordinatesExtensionsTests
{
    [Theory]
    [InlineData(1, 1, 3, 1)]
    [InlineData(1, 1, 1, 3)]
    [InlineData(9, 1, 3, 1)]
    [InlineData(1, 9, 1, 3)]
    public void IsInStraightLine_WhenCoordinatesAreInStraightLine_ShouldReturnTrue(int beginX, int beginY, int endX,
        int endY)
    {
        // Arrange
        var begin = Coordinates.At(beginX, beginY);
        var end = Coordinates.At(endX, endY);

        // Act
        var result = begin.IsInStraightLine(end);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(1, 1, 3, 4)]
    [InlineData(1, 1, 2, 2)]
    [InlineData(9, 1, 8, 2)]
    [InlineData(1, 9, 2, 3)]
    public void IsInStraightLine_WhenCoordinatesAreNotInStraightLine_ShouldReturnFalse(int beginX, int beginY, int endX,
        int endY)
    {
        // Arrange
        var begin = Coordinates.At(beginX, beginY);
        var end = Coordinates.At(endX, endY);

        // Act
        var result = begin.IsInStraightLine(end);

        // Assert
        result.Should().BeFalse();
    }
}