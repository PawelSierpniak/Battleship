namespace BattleshipGame.Domain.GameBoard.ShipsBoard;

public abstract class Ship
{
    protected Ship(string name, int width)
    {
        Name = name;
        Width = width;
        Hits = 0;
    }

    public string Name { get; }
    public int Width { get; }
    public int Hits { get; private set; }

    public void Hit()
    {
        Hits++;
    }

    public bool IsSunk => Hits >= Width;

    public override string ToString()
    {
        return Name.Substring(0, 1);
    }
}

public class Battleship : Ship
{
    public Battleship() : base("Battleship", 5)
    {
    }
}

public class Destroyers : Ship
{
    public Destroyers() : base("Destroyers", 4)
    {
    }
}