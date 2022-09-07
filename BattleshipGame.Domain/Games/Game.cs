using BattleshipGame.Domain.Display;
using BattleshipGame.Domain.GameBoard.ShipsBoard;
using BattleshipGame.Domain.Players;

namespace BattleshipGame.Domain.Games;

public class Game
{
    private readonly Player _player1;
    private readonly Player _player2;
    private readonly IBoardDisplay _boardDisplay;

    private Game(Player player1, Player player2, IBoardDisplay boardDisplay)
    {
        _player1 = player1;
        _player2 = player2;
        _boardDisplay = boardDisplay;
    }

    public void Play()
    {
        PlaceShip();

       _player1.ShowBoards(_boardDisplay);
        Gameloop();

        ShowResult();
    }

    private void ShowBoards()
    {

        _player2.ShowBoards(_boardDisplay);
    }

    private void ShowResult()
    {
        ShowBoards();

        if (_player1.HasLost)
        {
            Console.WriteLine(_player2.Name + " has won the game!");
        }
        else if (_player2.HasLost)
        {
            Console.WriteLine(_player1.Name + " has won the game!");
        }
    }

    private void Gameloop()
    {
        while (!_player1.HasLost && !_player2.HasLost)
        {
            PlayRound();
        }
    }

    private void PlayRound()
    {
        //PlayerOne play
        _boardDisplay.Clear();
        _player1.ShowBoards(_boardDisplay);

        var coordinates = _player1.Shot();
        var result = _player2.ProcessShot(coordinates);
        _player1.ProcessShotResult(coordinates, result);

        if (!_player2.HasLost) //If player 2 already lost, we can't let them take another turn.
        {
            coordinates = _player2.Shot();
            result = _player1.ProcessShot(coordinates);
            _player2.ProcessShotResult(coordinates, result);
        }
    }

    private void PlaceShip()
    {
        if (!_player2.HasPlacedShip())
        {
            _player2.PlaceShip();
        }

        if (!_player1.HasPlacedShip())
        {
            _player1.PlaceShip();
        }
    }

    public static Game BuildHumanVsComputerWith3ShipsAllAuto()
    {
        var display = new ConsoleBoardDisplay();
        var Ships = () => new List<Ship> { new Battleship(), new Battleship(), new Destroyers() };

        var palyer1 = new PlayerBuilder("Human", display, Ships)
            .WithAutoPutShipStrategy()
            .WithRandomShotStrategy()
            .Build();

        var palyer2 = new PlayerBuilder("Comuter", display, Ships)
            .WithAutoPutShipStrategy()
            .WithRandomShotStrategy()
            .Build();

        return new Game(palyer1, palyer2, display);
    }
    public static Game BuildHumanVsComputerWith3ShipsAutoPlace()
    {
        var display = new ConsoleBoardDisplay();
        var Ships = () => new List<Ship> { new Battleship(), new Battleship(), new Destroyers() };

        var palyer1 = new PlayerBuilder("Human", display, Ships)
            .WithAutoPutShipStrategy()
            .WithConsoleShotStrategy()
            .Build();

        var palyer2 = new PlayerBuilder("Comuter", display, Ships)
            .WithAutoPutShipStrategy()
            .WithRandomShotStrategy()
            .Build();

        return new Game(palyer1, palyer2, display);
    }

    public static Game BuildHumanVsComputerWith3Ships()
    {
        var display = new ConsoleBoardDisplay();
        var ShipFactory = () => new List<Ship> { new Battleship(), new Battleship(), new Destroyers() };

        var palyer1 = new PlayerBuilder("Human", display, ShipFactory)
        .WithAutoPutShipStrategy()
            .WithConsoleShotStrategy()
            .WithConsolePutShipStrategy()
            .Build();

        var palyer2 = new PlayerBuilder("Comuter", display, ShipFactory)
            .WithAutoPutShipStrategy()
            .WithRandomShotStrategy()
            .Build();

        return new Game(palyer1, palyer2, display);
    }
}