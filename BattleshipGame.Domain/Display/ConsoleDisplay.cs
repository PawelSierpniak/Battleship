using System.Text;
using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.FiringBoard;
using BattleshipGame.Domain.GameBoard.ShipsBoard;

namespace BattleshipGame.Domain.Display;

internal interface IBoardDisplay
{
    void Clear();

    public void DisplayBoard(ShipBoard shipBoard);
    public void PrintBoards(ShipBoard board, FireBoard fireBoard, string userName);
}

internal class ConsoleBoardDisplay : IBoardDisplay
{
    private const string BoardHeader = "|  | A| B| C| D| E| F| G| H| I| J|";
    private const string BoardSeparator = "             ";

    public void Clear()
    {
        Console.Clear();
    }

    public void DisplayBoard(ShipBoard shipBoard)
    {
        var sb = new StringBuilder(shipBoard.MapSize * 4);
        Console.WriteLine(BoardHeader);
        for (var y = 1; y <= shipBoard.MapSize; y++)
        {
            PrintRow(shipBoard, y, sb);
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }

        Console.WriteLine();
    }

    public void PrintBoards(ShipBoard board, FireBoard fireBoard, string userName)
    {
        Console.WriteLine($"Board {userName}");
        var sb = new StringBuilder(board.MapSize * 2 * 4);
        Console.WriteLine(BoardHeader + BoardSeparator + BoardHeader);
        for (var y = 1; y <= board.MapSize; y++)
        {
            PrintRow(board, y, sb);
            sb.Append(BoardSeparator);
            PrintRow(fireBoard, y, sb);
            Console.WriteLine(sb.ToString());
            sb.Clear();
        }

        Console.WriteLine();
    }

    private static void PrintRow<T>(BaseBoard<T> board, int y, StringBuilder sb)
        where T : class, IBoardElementBase, new()
    {
        for (var x = 0; x <= board.MapSize; x++)
        {
            var symbol = string.Empty;
            if (x > 0)
            {
                var a = board.GetElement(Coordinates.At(x, y));
                symbol = a.ToString();
            }
            else
            {
                symbol = y.ToString();
            }

            sb.Append($"|{symbol,2}");
        }

        sb.Append('|');
    }
}