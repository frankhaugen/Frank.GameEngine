using System.Drawing;
using System.Numerics;

namespace Frank.GameEngine.Primitives;

public class Grid<T>
{
    private readonly T[,] _board;
    
    public Grid(int width, int height) => _board = new T[width, height];

    public T this[int x, int y]
    {
        get => _board[x, y];
        set => _board[x, y] = value;
    }

    public T this[Point position]
    {
        get => _board[position.X, position.Y];
        set => _board[position.X, position.Y] = value;
    }
    
    public int Width => _board.GetLength(0);
    
    public int Height => _board.GetLength(1);
    
    public void Clear()
    {
        for (var x = 0; x < Width; x++)
        for (var y = 0; y < Height; y++)
            _board[x, y] = default!;
    }
}