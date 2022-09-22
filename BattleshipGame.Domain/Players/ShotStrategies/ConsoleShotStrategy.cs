using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.FiringBoard;

namespace BattleshipGame.Domain.Players.ShotStrategies;

internal class ConsoleShotStrategy : IShotStrategy
{
    public Coordinates FireShot(FireBoard fireBoard)
    {
        Coordinates? coordinates;
        do
        {
            coordinates = GetCoordinates();
        } while (coordinates == null);

        return coordinates;
    }

    private static Coordinates? GetCoordinates()
    {
        Console.WriteLine("Please put you shoot. Put in format like 'A2'");
        var position = Console.ReadLine();
        var result = Coordinates.TryParseAt(position);
        if (result.IsFailure)
        {
            Console.WriteLine(result.Error);
            return null;
        }

        return result.Value;
    }
}