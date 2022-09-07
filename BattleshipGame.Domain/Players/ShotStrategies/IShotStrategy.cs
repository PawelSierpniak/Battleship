using BattleshipGame.Domain.GameBoard;
using BattleshipGame.Domain.GameBoard.FiringBoard;

namespace BattleshipGame.Domain.Players.ShotStrategies;

internal interface IShotStrategy
{
    Coordinates FireShot(FireBoard fireBoard);
}