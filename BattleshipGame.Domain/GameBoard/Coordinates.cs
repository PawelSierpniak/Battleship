using System.Globalization;
using CSharpFunctionalExtensions;

namespace BattleshipGame.Domain.GameBoard;

internal class Coordinates
{
    public int X { get; }
    public int Y { get; }
    public static int MaxValue => Const.BoardSize;

    private static readonly int MinValue = 1;

    private static readonly int AIndexInCharTable = 65;

    public static Result<Coordinates> TryParseAt(string? coordinates)
    {
        if (coordinates == null)
        {
            return Result.Failure<Coordinates>("missing value");
        }

        if (coordinates.Length < 2)
        {
            return Result.Failure<Coordinates>("To short");
        }

        var positionX = coordinates[0];
        var x = ParseValue(positionX);
        if (!IsInRange(x))
        {
            return Result.Failure<Coordinates>($"Out of range value X {positionX}");
        }

        var positionY = coordinates.Substring(1);
        if (!int.TryParse(positionY, out var y))
        {
            return Result.Failure<Coordinates>("Incorrect x position. Need to by digits");
        }

        if (!IsInRange(y))
        {
            return Result.Failure<Coordinates>($"Out of range value Y {positionY}");
        }

        return new Coordinates(x, y);
    }

    private static int ParseValue(char positionX)
    {
        var x = char.ToUpper(positionX, CultureInfo.InvariantCulture) - AIndexInCharTable;
        // we want to start from 1 not from 0
        return x + 1;
    }

    public static Coordinates At(int x, int y)
    {
        CheckRangeAndThrow(x);
        CheckRangeAndThrow(y);

        return new Coordinates(x, y);
    }

    public static Result<Coordinates> TryAt(int x, int y)
    {
        if (!IsInRange(x))
        {
            return Result.Failure<Coordinates>("Value X is out of range");
        }

        if (!IsInRange(y))
        {
            return Result.Failure<Coordinates>("Value Y is out of range");
        }

        return new Coordinates(x, y);
    }

    private Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    private static bool IsInRange(int value)
    {
        if (value > MaxValue || value < MinValue)
        {
            return false;
        }

        return true;
    }

    private static void CheckRangeAndThrow(int value)
    {
        //TODO fix parameter name
        if (value > MaxValue || value < MinValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
    }

    public override string ToString()
    {
        return $"{XToString()}{Y}";
    }

    public string XToString()
    {
        return ((char)(X + AIndexInCharTable)).ToString();
    }

    #region Equals

    protected bool Equals(Coordinates other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((Coordinates)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    #endregion
}