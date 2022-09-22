namespace BattleshipGame.Domain.GameBoard;

internal abstract class BaseBoard<T> where T : class, IBoardElementBase, new()
{
    public int MapSize { get; }

    private readonly T[,] _map;

    protected BaseBoard()
    {
        MapSize = Const.BoardSize;
        _map = new T[MapSize + 1, MapSize + 1];

        for (var y = 1; y <= MapSize; y++)
        for (var x = 1; x <= MapSize; x++)
        {
            _map[x, y] = new T();
        }
    }

    public T GetElement(Coordinates coordinates)
    {
        return _map[coordinates.X, coordinates.Y];
    }

    public List<T> AllWithPredicate(Func<T, bool> predicate)
    {
        var result = new List<T>();

        for (var y = 1; y <= MapSize; y++)
        for (var x = 1; x <= MapSize; x++)
        {
            if (predicate(_map[x, y]))
            {
                result.Add(_map[x, y]);
            }
        }

        return result;
    }

    protected List<Coordinates> CoordinatesOfElementWithPredicate(Func<T, bool> predicate)
    {
        var result = new List<Coordinates>();

        for (var y = 1; y <= MapSize; y++)
        for (var x = 1; x <= MapSize; x++)
        {
            if (predicate(_map[x, y]))
            {
                result.Add(Coordinates.At(x, y));
            }
        }

        return result;
    }
}