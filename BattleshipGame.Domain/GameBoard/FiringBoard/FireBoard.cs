namespace BattleshipGame.Domain.GameBoard.FiringBoard;

internal class FireBoard : BaseBoard<FireSquare>
{
    public List<Coordinates> GetListOfEmptyCoordinates()
    {
        return CoordinatesOfElementWithPredicate(square => square.Status == FireSquare.FireSquareStatus.Empty);
    }
}