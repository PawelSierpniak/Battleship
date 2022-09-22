using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.ShipsBoard;
using CSharpFunctionalExtensions;

namespace BattleshipGame.Domain.Players.PutShipStrategies;

internal class AutoPutShipStrategy : IPutShipStrategy
{
    private const int MaxTryCount = 50;
    private readonly Random _rand = new(Guid.NewGuid().GetHashCode());

    public void PutAllShips(List<Ship> ships, ShipBoard board)
    {
        foreach (var ship in ships)
        {
            //Select a random row/column combination, then select a random orientation.
            //If none of the proposed panels are occupied, place the ship
            //Do this for all ships
            var tryCount = 1;
            var isOpen = true;
            while (isOpen)
            {
                if (tryCount++ > MaxTryCount)
                {
                    throw new ArgumentException("Can't put all ship on board");
                }

                var begin = board.GetRandomPosition();
                var end = TryCalculateEnd(ship, begin);
                if (end.IsFailure)
                {
                    isOpen = true;
                    continue;
                }

                var tryResult = board.TryPutShip(ship, begin, end.Value);
                if (tryResult.IsFailure)
                {
                    isOpen = true;
                    continue;
                }

                isOpen = false;
            }
        }
    }

    private Result<Coordinates> TryCalculateEnd(Ship ship, Coordinates begin)
    {
        var orientation = _rand.Next(1, 101) % 2; //0 for Horizontal
        var endX = begin.X;
        var endY = begin.Y;

        if (orientation == 0)
        {
            endX += ship.Width - 1;
        }
        else
        {
            endY += ship.Width - 1;
        }

        return Coordinates.TryAt(endX, endY);
    }
}