using System.Numerics;

namespace Frank.GameEngine.Primitives.SubPrimitives;

public static class Array2DExtensions
{
    public static T? GetValue<T>(this T?[,] array, ArrayPosition2D position)
    {
        return array[position.Y, position.X];
    }

    public static T? GetValue<T>(this T?[,] array, uint x, uint y)
    {
        return array[y, x];
    }

    public static T? GetValue<T>(this T?[,] array, string x, string y)
    {
        var position = new ArrayPosition2D(x, y);
        return array[position.Y, position.X];
    }

    public static T? GetValue<T>(this T?[,] array, uint x, string y)
    {
        var position = new ArrayPosition2D(x, y);
        return array[position.Y, position.X];
    }

    public static T? GetValue<T>(this T?[,] array, string x, uint y)
    {
        var position = new ArrayPosition2D(x, y);
        return array[position.Y, position.X];
    }

    public static IEnumerable<T?> GetValues<T>(this T?[,] array)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
        for (var j = 0u; j < array.GetLength(1); j++)
            yield return array[i, j];
    }

    public static bool TryGetValue<T>(this T?[,] array, ArrayPosition2D position, out T? value)
    {
        value = array[position.Y, position.X];
        return value != null;
    }

    public static bool TryGetValue<T>(this T?[,] array, uint x, uint y, out T? value)
    {
        value = array[y, x];
        return value != null;
    }

    public static bool TryGetValue<T>(this T?[,] array, string x, string y, out T? value)
    {
        var position = new ArrayPosition2D(x, y);
        value = array[position.Y, position.X];
        return value != null;
    }

    public static IEnumerable<T?> GetRow<T>(this T?[,] array, uint row)
    {
        for (var i = 0u; i < array.GetLength(1); i++)
            yield return array[row, i];
    }

    public static IEnumerable<T?> GetColumn<T>(this T?[,] array, uint column)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
            yield return array[i, column];
    }

    public static IEnumerable<T?> GetRow<T>(this T?[,] array, string row)
    {
        var position = new ArrayPosition2D(row, 0);
        for (var i = 0u; i < array.GetLength(1); i++)
            yield return array[position.Y, i];
    }

    public static IEnumerable<T?> GetColumn<T>(this T?[,] array, string column)
    {
        var position = new ArrayPosition2D(0, column);
        for (var i = 0u; i < array.GetLength(0); i++)
            yield return array[i, position.X];
    }

    public static IEnumerable<T?> Find<T>(this T?[,] array, Func<T, bool> predicate)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
        for (var j = 0u; j < array.GetLength(1); j++)
            if (array[i, j] != null && predicate(array[i, j]!))
                yield return array[i, j]!;
    }

    public static IEnumerable<T?> FindInRow<T>(this T?[,] array, uint row, Func<T?, bool> predicate)
    {
        for (var i = 0u; i < array.GetLength(1); i++)
            if (array[row, i] != null && predicate(array[row, i]))
                yield return array[row, i];
    }

    public static IEnumerable<T?> FindInColumn<T>(this T?[,] array, uint column, Func<T?, bool> predicate)
    {
        for (var i = 0u; i < array.GetLength(0); i++)
            if (array[i, column] != null && predicate(array[i, column]))
                yield return array[i, column];
    }

    public static IEnumerable<T?> FindIn<T>(this T?[,] array, Func<T, bool> predicate, params ArrayPosition2D[] positions)
    {
        foreach (var position in positions)
            if (array[position.Y, position.X] != null && predicate(array[position.Y, position.X]!))
                yield return array[position.Y, position.X]!;
    }

    public static IEnumerable<T?> FindIn<T>(this T?[,] array, Func<T, bool> predicate, params Vector2[] positions)
    {
        foreach (var position in positions)
            if (array[(uint)position.Y, (uint)position.X] != null && predicate(array[(uint)position.Y, (uint)position.X]!))
                yield return array[(uint)position.Y, (uint)position.X]!;
    }
}