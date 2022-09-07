using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.FiringBoard;

namespace BattleshipGame.Domain.Players.ShotStrategies;

internal class RandomShotStrategy : IShotStrategy
{
    public Coordinates FireShot(FireBoard fireBoard)
    {
        var availableCoordinates = fireBoard.GetListOfEmptyCoordinates();
        var rand = new Random(Guid.NewGuid().GetHashCode());
        var coordinates = rand.Next(availableCoordinates.Count);
        return availableCoordinates[coordinates];
    }
}