using CSharpFunctionalExtensions;

namespace BattleshipGame.Domain.GameBoard;

internal class Coordinates
{
    public int X { get; }
    public int Y { get; }
    public static int MaxValue => Const.BoardSize;
    private static readonly int MinValue = 1;

    private static readonly string XAllowedValues = "ABCDEFGHIJ";
    private static readonly string XTranslationArray = "_" + XAllowedValues;

    public static Result<Coordinates> TryParseAt(string? coordinates)
    {
        if (coordinates == null)
        {
            return Result.Failure<Coordinates>("missing value");
        }

        if (coordinates.Length < 2)
        {
            return Result.Failure<Coordinates>("To shor");
        }

        //TODO: refoctor this;
        var xAsChar = coordinates[0];
        if (XAllowedValues.IndexOf(xAsChar) < 0)
        {
            return Result.Failure<Coordinates>($"Incorrect first letter. Need to be {XAllowedValues}");
        }

        if (!int.TryParse(coordinates.AsSpan(1), out var y))
        {
            return Result.Failure<Coordinates>("Incorrect x position. Need to by digits");
        }

        if (!IsInRange(y))
        {
            return Result.Failure<Coordinates>($"Out of range value Y {y}");
        }

        var x = XTranslationArray.IndexOf(xAsChar);
        if (x < 1)
        {
            return Result.Failure<Coordinates>($"Out of range value Y {y}");
        }

        return new Coordinates(x, y);
    }

    public static Coordinates At(int x, int y)
    {
        CheckRangeAndThrow(x);
        CheckRangeAndThrow(y);

        return new Coordinates(x, y);
    }

    private Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    private static bool IsInRange(int value)
    {
        //TODO fix parameter name
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
        return $"{XTranslationArray[X]}{Y}";
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