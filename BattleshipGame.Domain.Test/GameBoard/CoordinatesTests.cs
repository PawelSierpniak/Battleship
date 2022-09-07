using BattleshipGame.Domain.GameBoard;
using FluentAssertions;

namespace BattleshipGame.Domain.Test.GameBoard;

public class CoordinatesTests
{
    [Theory]
    [MemberData(nameof(CorrectValues))]
    public void TryParseAt_ShouldParseCorrectCoordinates(string value, int x, int y)
    {
        // Arrange/Act
        var coordinates = Coordinates.TryParseAt(value);

        // Assert
        coordinates.IsSuccess.Should().BeTrue();
        coordinates.Value.X.Should().Be(x);
        coordinates.Value.Y.Should().Be(y);
    }

    [Theory]
    [InlineData("A0")]
    [InlineData("W2")]
    [InlineData("_")]
    [InlineData("A1a ")]
    [InlineData("B11")]
    [InlineData("B")]
    [InlineData("D-1")]
    public void TryParseAt_ShouldasParseCorrectCoordinates(string value)
    {
        // Arrange/Act
        var coordinates = Coordinates.TryParseAt(value);

        // Assert
        coordinates.IsSuccess.Should().BeFalse();
    }

    public static IEnumerable<object[]> CorrectValues()
    {
        var allData = new List<object[]>
        {
            new object[] { "A1", 1, 1 },
            new object[] { "B2", 2, 2 },
            new object[] { "C3", 3, 3 },
            new object[] { "D4", 4, 4 },
            new object[] { "E5", 5, 5 },
            new object[] { "F6", 6, 6 },
            new object[] { "G7", 7, 7 },
            new object[] { "H8", 8, 8 },
            new object[] { "I9", 9, 9 },
            new object[] { "J10", 10, 10 }
        };
        return allData;
    }
}