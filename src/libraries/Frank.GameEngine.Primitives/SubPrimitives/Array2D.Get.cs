namespace Frank.GameEngine.Primitives.SubPrimitives;

public partial class Array2D<T>
{
    public T? Get(ArrayPosition2D position)
    {
        return _array[position.Y, position.X];
    }

    public T? Get(uint x, uint y)
    {
        return _array[y, x];
    }

    public T? Get(string x, string y)
    {
        var position = new ArrayPosition2D(x, y);
        return _array[position.Y, position.X];
    }

    public T? Get(uint x, string y)
    {
        var position = new ArrayPosition2D(x, y);
        return _array[position.Y, position.X];
    }

    public T? Get(string x, uint y)
    {
        var position = new ArrayPosition2D(x, y);
        return _array[position.Y, position.X];
    }

    public T?[] GetRow(uint row)
    {
        var rowArray = new T?[Width];
        for (var i = 0u; i < Width; i++) rowArray[i] = _array[row, i];
        return rowArray;
    }

    public T?[] GetColumn(uint column)
    {
        var columnArray = new T?[Height];
        for (var i = 0u; i < Height; i++) columnArray[i] = _array[i, column];
        return columnArray;
    }

    public T?[] GetRow(string row)
    {
        var rowArray = new T?[Width];
        var position = new ArrayPosition2D(row, 0);
        for (var i = 0u; i < Width; i++) rowArray[i] = _array[position.Y, i];
        return rowArray;
    }

    public T?[] GetColumn(string column)
    {
        var columnArray = new T?[Height];
        var position = new ArrayPosition2D(0, column);
        for (var i = 0u; i < Height; i++) columnArray[i] = _array[i, position.X];
        return columnArray;
    }
}