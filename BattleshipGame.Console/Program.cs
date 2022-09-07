using BattleshipGame.Domain.Games;

namespace BattleshipGame.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var newGame = Game.BuildHumanVsComputerWith3ShipsAutoPlace();
            newGame.Play();

            System.Console.ReadKey();
        }
    }
}