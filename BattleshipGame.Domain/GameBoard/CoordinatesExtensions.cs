namespace BattleshipGame.Domain.GameBoard;

internal static class CoordinatesExtensions
{
    public static bool IsInStraightLine(this Coordinates begin, Coordinates end)
    {
        if (HasThisSameX(begin, end))
        {
            return true;
        }

        if (HasThisSameY(begin, end))
        {
            return true;
        }

        return false;
    }

    public static bool HasThisSameY(this Coordinates begin, Coordinates end)
    {
        if (begin.Y == end.Y)
        {
            return true;
        }

        return false;
    }

    public static bool HasThisSameX(this Coordinates begin, Coordinates end)
    {
        if (begin.X == end.X)
        {
            return true;
        }

        return false;
    }

    public static List<Coordinates> GetCoordinatesList(this Coordinates begin, Coordinates end)
    {
        var result = new List<Coordinates> { begin };

        if (begin == end)
        {
            return result;
        }

        result.AddRange(GetBetween(begin, end)!);
        result.Add(end);
        return result;
    }

    public static List<Coordinates>? GetBetween(Coordinates begin, Coordinates end)
    {
        if (!IsInStraightLine(begin, end))
        {
            return null; //TODO: DO it better
        }

        var result = new List<Coordinates>();
        if (HasThisSameX(begin, end))
        {
            var max = Math.Max(begin.Y, end.Y);
            var min = Math.Min(begin.Y, end.Y);
            for (var i = min + 1; i < max; i++)
            {
                result.Add(Coordinates.At(begin.X, i));
            }
        }

        if (HasThisSameY(begin, end))
        {
            var max = Math.Max(begin.X, end.X);
            var min = Math.Min(begin.X, end.X);
            for (var i = min + 1; i < max; i++)
            {
                result.Add(Coordinates.At(i, begin.Y));
            }
        }

        return result;
    }
}