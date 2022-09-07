using BattleshipGame.Domain.Players.PutShipStrategies;
using FluentAssertions;

namespace BattleshipGame.Domain.Test.Players.PutShipStrategies;

public class AutoPutShipStrategyTests
{
    private readonly AutoPutShipStrategy _sut;

    public AutoPutShipStrategyTests()
    {
        _sut = new AutoPutShipStrategy();
    }

    [Fact]
    public void PutAllShips_ShouldSetAllShipSquareAsIsOccupied()
    {
        // Arrange
        var (board, ships) = new ShipBoardScenario()
            .WithEmptyBoard()
            .WithBattleshipsInNumber(5)
            .Build();

        // Act
        _sut.PutAllShips(ships, board);

        // Assert
        board.ShouldHaveOccupiedSquaresCount(ships.GetSumOfAllWith());
    }

    [Fact]
    public void PutAllShips_WhenCaNotPlaceAllShip_ShouldThrowException()
    {
        // Arrange
        var (board, ships) = new ShipBoardScenario()
            .WithEmptyBoard()
            .WithBattleshipsInNumber(200)
            .Build();

        // Act
        var act = () => _sut.PutAllShips(ships, board);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}