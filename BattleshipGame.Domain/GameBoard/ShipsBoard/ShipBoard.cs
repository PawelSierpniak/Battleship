using CSharpFunctionalExtensions;

namespace BattleshipGame.Domain.GameBoard.ShipsBoard;

internal class ShipBoard : BaseBoard<ShipSquare>
{
    private readonly Random _rand = new(Guid.NewGuid().GetHashCode());

    //TODO: Coordinates begin, Coordinates end look like candidate to own class
    public Result CanPlaceShip(Ship ship, Coordinates begin, Coordinates end)
    {
        if (!begin.IsInStraightLine(end))
        {
            return Result.Failure<List<Coordinates>>("Ship Coordinates need to be in straight line");
        }

        if (!HasLenghtEqualShip(ship.Width, begin, end))
        {
            return Result.Failure($"Coordinates are incorrect. {ship.Name} occupied {ship.Width} fields");
        }

        if (AreOccupied(begin, end))
        {
            return Result.Failure("All fields need to be empty");
        }

        return Result.Success();
    }

    private bool AreOccupied(Coordinates begin, Coordinates end)
    {
        var list = begin.GetCoordinatesList(end);

        foreach (var n in list)
        {
            if (GetElement(n).IsOccupied)
            {
                return true;
            }
        }

        return false;
    }

    private static bool HasLenghtEqualShip(int shipWidth, Coordinates begin, Coordinates end)
    {
        if (begin.HasThisSameX(end) && Math.Abs(begin.Y - end.Y) == shipWidth - 1)
        {
            return true;
        }

        if (begin.HasThisSameY(end) && Math.Abs(begin.X - end.X) == shipWidth - 1)
        {
            return true;
        }

        return false;
    }

    internal Result TryPutShip(Ship ship, Coordinates begin, Coordinates end)
    {
        var result = CanPlaceShip(ship, begin, end);
        if (result.IsFailure)
        {
            return result;
        }

        PutShip(ship, begin, end);

        return Result.Success();
    }

    private void PutShip(Ship ship, Coordinates begin, Coordinates end)
    {
        foreach (var n in begin.GetCoordinatesList(end))
        {
            GetElement(n).PutShip(ship);
        }
    }

    public Coordinates GetRandomPosition()
    {
        var startX = _rand.Next(1, Const.BoardSize + 1);
        var startY = _rand.Next(1, Const.BoardSize + 1);

        return Coordinates.At(startX, startY);
    }
}