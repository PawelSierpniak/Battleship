namespace BattleshipGame.Domain.GameBoard.FiringBoard;

internal class FireSquare : IBoardElementBase
{
    public enum FireSquareStatus
    {
        Empty,
        Miss,
        Hit,
        Sunk
    }

    public FireSquareStatus Status { get; private set; }

    public FireSquare()
    {
        Status = FireSquareStatus.Empty;
    }

    public override string ToString()
    {
        return Status switch
        {
            FireSquareStatus.Empty => " ",
            FireSquareStatus.Miss => "M",
            FireSquareStatus.Hit => "X",
            FireSquareStatus.Sunk => "S",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void ProcessShotResult(FireResult result)
    {
        switch (result)
        {
            case FireResult.Miss:
                Status = FireSquareStatus.Miss;
                return;
            case FireResult.Hit:
                Status = FireSquareStatus.Hit;
                return;
            case FireResult.Sunk:
                Status = FireSquareStatus.Sunk;
                return;
            default:
                throw new ArgumentOutOfRangeException(nameof(result), result, null);
        }
    }
}