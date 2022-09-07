using BattleshipGame.Domain.GameBoard.ShipsBoard;

namespace BattleshipGame.Domain.Players.PutShipStrategies;

internal class AutoPutShipStrategy : IPutShipStrategy
{
    // Stolen from https://github.com/exceptionnotfound/BattleshipModellingPractice
    // TODO: need to be refactor

    private const int MaxTryCount = 50;

    public void PutAllShips(List<Ship> ships, ShipBoard board)
    {
        //Random class creation stolen from http://stackoverflow.com/a/18267477/106356
        var rand = new Random(Guid.NewGuid().GetHashCode());
        foreach (var ship in ships)
        {
            //Select a random row/column combination, then select a random orientation.
            //If none of the proposed panels are occupied, place the ship
            //Do this for all ships
            var tryCount = 1;
            var isOpen = true;
            while (isOpen)
            {
                if (tryCount++ > MaxTryCount)
                {
                    throw new ArgumentException("Can't put all ship on board");
                }

                var startcolumn = rand.Next(1, 11);
                var startrow = rand.Next(1, 11);
                int endrow = startrow, endcolumn = startcolumn;
                var orientation = rand.Next(1, 101) % 2; //0 for Horizontal

                if (orientation == 0)
                {
                    for (var i = 1; i < ship.Width; i++)
                    {
                        endrow++;
                    }
                }
                else
                {
                    for (var i = 1; i < ship.Width; i++)
                    {
                        endcolumn++;
                    }
                }

                //We cannot place ships beyond the boundaries of the board
                if (endrow > 10 || endcolumn > 10)
                {
                    isOpen = true;
                    continue;
                }

                //Check if specified panels are occupied
                var affectedPanels = board.Range(startrow, startcolumn, endrow, endcolumn);
                if (affectedPanels.Any(x => x.IsOccupied))
                {
                    isOpen = true;
                    continue;
                }

                foreach (var panel in affectedPanels)
                {
                    panel.PutShip(ship);
                }

                isOpen = false;
            }
        }
    }
}