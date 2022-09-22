using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.FiringBoard;

namespace BattleshipGame.Domain.Players.ShotStrategies;

internal class RandomShotStrategy : IShotStrategy
{
    private readonly Random _rand = new(Guid.NewGuid().GetHashCode());

    public Coordinates FireShot(FireBoard fireBoard)
    {
        var availableCoordinates = fireBoard.GetListOfEmptyCoordinates();

        var coordinates = _rand.Next(availableCoordinates.Count);
        return availableCoordinates[coordinates];
    }
}