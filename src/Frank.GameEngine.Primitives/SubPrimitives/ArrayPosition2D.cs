namespace Frank.GameEngine.Primitives.SubPrimitives;

public readonly record struct ArrayPosition2D
{
    public uint X { get; }
    public uint Y { get; }

    /// <summary>
    ///    Creates an array position from a y and x.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public ArrayPosition2D(uint x, uint y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    ///   Creates an array position from a y and x.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public ArrayPosition2D(string x, string y)
    {
        X = ConvertToZeroBasedIndex(x);
        Y = ConvertToZeroBasedIndex(y);
    }

    /// <summary>
    ///  Creates an array position from a y and x.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public ArrayPosition2D(uint x, string y)
    {
        Y = ConvertToZeroBasedIndex(y);
        X = x;
    }

    /// <summary>
    /// Creates an array position from a y and x.
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    public ArrayPosition2D(string x, uint y)
    {
        Y = y;
        X = ConvertToZeroBasedIndex(x);
    }

    private static uint ConvertToZeroBasedIndex(string index)
    {
        var result = 0u;
        foreach (var c in index)
            if (char.IsLetter(c))
                result = result * 26u + (uint)(c - 'a');
            else
                throw new ArgumentException("Invalid character in index");
        return result;
    }
}