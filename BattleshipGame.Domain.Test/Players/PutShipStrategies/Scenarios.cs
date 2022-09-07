using BattleshipGame.Domain.GameBoard.ShipsBoard;
using FluentAssertions;

internal class ShipBoardScenario
{
    private static ShipBoard EmptyShipBoard => new();
    private static List<Ship> OneBattleship => new() { new Battleship() };

    private ShipBoard _board;
    private List<Ship> _ships;

    public ShipBoardScenario()
    {
        _board = EmptyShipBoard;
        _ships = new List<Ship>();
    }

    public ShipBoardScenario WithEmptyBoard()
    {
        _board = EmptyShipBoard;

        return this;
    }

    public ShipBoardScenario WithOneBattleship()
    {
        _ships = OneBattleship;
        return this;
    }

    public ShipBoardScenario WithBattleshipsInNumber(int count)
    {
        _ships = new List<Ship>();
        for (var i = 0; i < count; i++)
        {
            _ships.Add(new Battleship());
        }

        return this;
    }

    public (ShipBoard board, List<Ship> ships) Build()
    {
        return (_board, _ships);
    }
}

internal static class ShipBoardScenarioVerification
{
    internal static void ShouldHaveOccupiedSquaresCount(this ShipBoard board, int squaresCount)
    {
        board.AllWithPredicate(square => square.IsOccupied).Count.Should()
            .Be(squaresCount);
    }
}

internal static class ShipListExtensions
{
    internal static int GetSumOfAllWith(this List<Ship> ships)
    {
        return ships.Select(a => a.Width).Sum(a => a);
    }
}