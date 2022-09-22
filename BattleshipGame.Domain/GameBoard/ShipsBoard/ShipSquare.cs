namespace BattleshipGame.Domain.GameBoard.ShipsBoard;

public class ShipSquare : IBoardElementBase
{
    private Ship? _ship;
    private bool _wasShoot;

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
        _wasShoot = true;

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
            if (_wasShoot & !_ship.IsSunk)
            {
                return _ship + "X";
            }

            if (_wasShoot & _ship.IsSunk)
            {
                return "S";
            }

            return _ship.ToString();
        }

        if (_wasShoot)
        {
            return "X";
        }

        return " ";
    }
}