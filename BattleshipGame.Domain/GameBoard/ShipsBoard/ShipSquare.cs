namespace BattleshipGame.Domain.GameBoard.ShipsBoard;

public class ShipSquare : IBoardElementBase
{
    private Ship? _ship;

    public bool IsOccupied => _ship != null;

    public ShipSquare()
    {
        _ship = null;
    }

    public void PutShip(Ship ship)
    {
        if (IsOccupied)
        {
            throw new ArgumentException("Can't put 2 ship in this same place");
        }

        _ship = ship;
    }

    public FireResult ProcessShot()
    {
        if (IsOccupied)
        {
            _ship!.Hit();

            if (_ship.IsSunk)
            {
                return FireResult.Sunk;
            }

            return FireResult.Hit;
        }

        return FireResult.Miss;
    }

    public override string ToString()
    {
        if (_ship != null)
        {
            return _ship.ToString();
        }
        //
        // if (_wasShoot)
        // {
        //     return "X";
        // }

        return " ";
    }
}