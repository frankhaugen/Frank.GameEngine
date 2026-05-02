namespace Frank.GameEngine.Primitives.SubPrimitives;

// File src/Frank.GameEngine.Primitives/SubPrimitives/Array2D.Indexers.cs
public partial class Array2D<T>
{
    public T? this[ArrayPosition2D position]
    {
        get => _array[position.Y, position.X];
        set => _array[position.Y, position.X] = value;
    }

    public T? this[uint x, uint y]
    {
        get => _array[y, x];
        set => _array[y, x] = value;
    }

    public T? this[string x, string y]
    {
        get => this[new ArrayPosition2D(x, y)];
        set => this[new ArrayPosition2D(x, y)] = value;
    }

    public T? this[uint x, string y]
    {
        get => this[new ArrayPosition2D(x, y)];
        set => this[new ArrayPosition2D(x, y)] = value;
    }

    public T? this[string x, uint y]
    {
        get => this[new ArrayPosition2D(x, y)];
        set => this[new ArrayPosition2D(x, y)] = value;
    }
}