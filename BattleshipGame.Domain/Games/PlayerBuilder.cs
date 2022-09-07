using BattleshipGame.Domain.Display;
using BattleshipGame.Domain.GameBoard.ShipsBoard;
using BattleshipGame.Domain.Players;
using BattleshipGame.Domain.Players.PutShipStrategies;
using BattleshipGame.Domain.Players.ShotStrategies;

namespace BattleshipGame.Domain.Games;

internal class PlayerBuilder
{
    private readonly Func<List<Ship>> _shipFactory;
    private IShotStrategy _shotStrategy;
    private IPutShipStrategy _putShipStrategy;
    private readonly string _name;
    private readonly ConsoleBoardDisplay _display;

    public PlayerBuilder(string name, ConsoleBoardDisplay display, Func<List<Ship>> shipFactory)
    {
        _shipFactory = shipFactory;
        _name = name;
        _display = display;
        _shotStrategy = new RandomShotStrategy();
        _putShipStrategy = new AutoPutShipStrategy();
    }

    public PlayerBuilder WithRandomShotStrategy()
    {
        _shotStrategy = new RandomShotStrategy();
        return this;
    }

    public PlayerBuilder WithConsoleShotStrategy()
    {
        _shotStrategy = new ConsoleShotStrategy();
        return this;
    }

    public PlayerBuilder WithAutoPutShipStrategy()
    {
        _putShipStrategy = new AutoPutShipStrategy();
        return this;
    }

    public PlayerBuilder WithConsolePutShipStrategy()
    {
        _putShipStrategy = new ConsolePutShipStrategy(_display);
        return this;
    }

    public Player Build()
    {
        return new Player(_name, _putShipStrategy, _shipFactory(), _shotStrategy);
    }
}