using BattleshipGame.Domain.Display;
using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.ShipsBoard;
using CSharpFunctionalExtensions;

namespace BattleshipGame.Domain.Players.PutShipStrategies;

internal interface IPositionReader
{
    string? ReadUserInput();
}

internal class ConsolePositionReader : IPositionReader
{
    public string? ReadUserInput()
    {
        return Console.ReadLine();
    }
}

internal class ConsolePutShipStrategy : IPutShipStrategy
{
    private readonly ConsoleBoardDisplay _consoleBoardDisplay;
    private readonly IPositionReader _positionReader;

    private string? ReadPosition()
    {
        return _positionReader.ReadUserInput();
    }

    public ConsolePutShipStrategy(ConsoleBoardDisplay consoleBoardDisplay)
    {
        _consoleBoardDisplay = consoleBoardDisplay;
        _positionReader = new ConsolePositionReader();
    }

    public ConsolePutShipStrategy(ConsoleBoardDisplay consoleBoardDisplay, IPositionReader positionReader)
    {
        _consoleBoardDisplay = consoleBoardDisplay;
        _positionReader = positionReader;
    }

    public void PutAllShips(List<Ship> ships, ShipBoard board)
    {
        foreach (var ship in ships)
        {
            Console.WriteLine($"Please put {ship.Name}");
            _consoleBoardDisplay.DisplayBoard(board);
            Result result;
            do
            {
                var cord = GetShipCoordinates();

                result = PutShip(ship, cord.start, cord.End, board);
                if (result.IsFailure)
                {
                    Console.WriteLine(result.Error);
                }
            } while (result.IsFailure);
        }
    }

    private (Coordinates start, Coordinates End) GetShipCoordinates()
    {
        Console.WriteLine("Get ship start position");
        Coordinates? start;
        do
        {
            start = GetCoordinates();
        } while (start == null);

        Console.WriteLine("Get ship End position");

        Coordinates? end;
        do
        {
            end = GetCoordinates();
        } while (end == null);

        return (start!, end!);
    }

    private Coordinates? GetCoordinates()
    {
        var positonStart = ReadPosition();
        var result = Coordinates.TryParseAt(positonStart);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return null;
        }

        return result.Value;
    }

    private static Result PutShip(Ship ship, Coordinates begin, Coordinates end, ShipBoard board)
    {
        var result = CheckIfShipCanBePlaced(ship, begin, end, board);
        if (result.IsFailure)
        {
            return result;
        }

        var list = begin.GetCoordinatesList(end);
        if (list.IsFailure)
        {
            return Result.Failure(list.Error);
        }

        foreach (var n in list.Value)
        {
            board.GetElement(n).PutShip(ship);
        }

        return Result.Success();
    }

    private static Result CheckIfShipCanBePlaced(Ship ship, Coordinates begin, Coordinates end, ShipBoard board)
    {
        if (!ValidateCoordinate(ship.Width, begin, end))
        {
            return Result.Failure($"Coordinates are incorrect. {ship.Name} occupied {ship.Width} fields");
        }

        //sprawdz czy pola są puste
        var list = begin.GetCoordinatesList(end);
        if (list.IsFailure)
        {
            return Result.Failure(list.Error);
        }

        foreach (var n in list.Value)
        {
            if (board.GetElement(n).IsOccupied)
            {
                return Result.Failure("All fields  need to be empty");
            }
        }

        var Neighbors = CoordinatesExtensions.GetNeighbors(list.Value);
        foreach (var n in Neighbors)
        {
            if (board.GetElement(n).IsOccupied)
            {
                return Result.Failure("All fields Neighbors need to be empty");
            }
        }

        return Result.Success();
    }

    private static bool ValidateCoordinate(int shipWidth, Coordinates begin, Coordinates end)
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
}