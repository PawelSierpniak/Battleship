using BattleshipGame.Domain.Display;
using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.FiringBoard;
using BattleshipGame.Domain.GameBoard.ShipsBoard;
using BattleshipGame.Domain.Players.PutShipStrategies;
using BattleshipGame.Domain.Players.ShotStrategies;

namespace BattleshipGame.Domain.Players;

internal class Player
{
    public string Name { get; }

    private ShipBoard Board { get; }

    private FireBoard FireBoard { get; }

    private readonly IPutShipStrategy _iPutShipStrategy;
    private List<Ship> _ships { get; }
    public bool HasLost => _ships.All(x => x.IsSunk);

    private bool _shipPlaced;
    private readonly IShotStrategy _iShotStrategy;

    public Player(string name, IPutShipStrategy iPutShipStrategy, List<Ship> ships,
        IShotStrategy iShotStrategy)
    {
        Name = name;
        Board = new ShipBoard();
        FireBoard = new FireBoard();
        _iPutShipStrategy = iPutShipStrategy;
        _ships = ships;
        _iShotStrategy = iShotStrategy;
        _shipPlaced = false;
    }

    public bool HasPlacedShip()
    {
        return _shipPlaced;
    }

    public void PlaceShip()
    {
        _iPutShipStrategy.PutAllShips(_ships, Board);
        _shipPlaced = true;
    }

    public void ShowBoards(IBoardDisplay boardDisplay)
    {
        boardDisplay.PrintBoards(Board, FireBoard, Name);
    }

    public Coordinates Shot()
    {
        return _iShotStrategy.FireShot(FireBoard);
    }

    public FireResult ProcessShot(Coordinates coordinates)
    {
        var element = Board.GetElement(coordinates);
        return element.ProcessShot();
    }

    public void ProcessShotResult(Coordinates coordinates, FireResult result)
    {
        var element = FireBoard.GetElement(coordinates);
        element.ProcessShotResult(result);
    }
}