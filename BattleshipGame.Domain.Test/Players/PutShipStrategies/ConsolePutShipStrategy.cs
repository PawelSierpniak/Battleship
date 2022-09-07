using BattleshipGame.Domain.Display;
using BattleshipGame.Domain.Players.PutShipStrategies;
using Moq;

namespace BattleshipGame.Domain.Test.Players.PutShipStrategies;

public class ConsolePutShipStrategyTests
{
    private readonly ConsolePutShipStrategy _sut;
    private readonly Mock<IPositionReader> _positionReaderMoq;

    public ConsolePutShipStrategyTests()
    {
        _positionReaderMoq = new Mock<IPositionReader>();
        _sut = new ConsolePutShipStrategy(new ConsoleBoardDisplay(), _positionReaderMoq.Object);
    }

    [Fact]
    public void PutAllShips_ShouldSetAllShipSquareAsIsOccupied()
    {
        // Arrange
        var (board, ships) = new ShipBoardScenario()
            .WithEmptyBoard()
            .WithOneBattleship()
            .Build();

        // Act
        UserInputs("A1", "A5");
        _sut.PutAllShips(ships, board);

        // Assert
        board.ShouldHaveOccupiedSquaresCount(ships.GetSumOfAllWith());
    }

    private void UserInputs(params string[] userInputs)
    {
        var queue = new Queue<string>(userInputs);
        _positionReaderMoq.Setup(r => r.ReadUserInput())
            .Returns(queue.Dequeue);
    }
}