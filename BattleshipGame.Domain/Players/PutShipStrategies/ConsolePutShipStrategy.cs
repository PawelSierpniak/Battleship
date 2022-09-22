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

                result = board.TryPutShip(ship, cord.start, cord.End);
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
        var position = ReadPosition();
        var result = Coordinates.TryParseAt(position);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return null;
        }

        return result.Value;
    }
}