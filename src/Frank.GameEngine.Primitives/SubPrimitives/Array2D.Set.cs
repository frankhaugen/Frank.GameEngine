namespace Frank.GameEngine.Primitives.SubPrimitives;

public partial class Array2D<T>
{
    public void Set(ArrayPosition2D position, T value)
    {
        _array[position.Y, position.X] = value;
    }

    public void Set(uint x, uint y, T value)
    {
        _array[y, x] = value;
    }

    public void Set(string x, string y, T value)
    {
        var position = new ArrayPosition2D(x, y);
        _array[position.Y, position.X] = value;
    }

    public void Set(uint x, string y, T value)
    {
        var position = new ArrayPosition2D(x, y);
        _array[position.Y, position.X] = value;
    }

    public void Set(string x, uint y, T value)
    {
        var position = new ArrayPosition2D(x, y);
        _array[position.Y, position.X] = value;
    }
}