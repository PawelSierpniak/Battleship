using BattleshipGame.Domain.GameBoard.ShipsBoard;

namespace BattleshipGame.Domain.Players.PutShipStrategies;

internal interface IPutShipStrategy
{
    void PutAllShips(List<Ship> ships, ShipBoard board);
}