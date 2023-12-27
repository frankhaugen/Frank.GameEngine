namespace Frank.GameEngine.Primitives;

public readonly record struct BoardPosition
{
    public int Row { get; }
    public int Column { get; }

    /// <summary>
    ///    Creates a board position from a row and column.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public BoardPosition(int row, int column)
    {
        Row = row;
        Column = column;
        Validate();
    }

    /// <summary>
    ///   Creates a board position from a row and column.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public BoardPosition(string row, string column)
    {
        Row = ConvertToZeroBasedIndex(row);
        Column = ConvertToZeroBasedIndex(column);
        Validate();
    }

    /// <summary>
    ///  Creates a board position from a row and column.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public BoardPosition(int row, string column)
    {
        Row = row;
        Column = ConvertToZeroBasedIndex(column);
        Validate();
    }
    
    /// <summary>
    /// Creates a board position from a row and column.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="column"></param>
    public BoardPosition(string row, int column)
    {
        Row = ConvertToZeroBasedIndex(row);
        Column = column;
        Validate();
    }

    private void Validate()
    {
        ValidateIndexes(Row, Column);
    }
    
    private static void ValidateIndexes(int row, int column)
    {
        ValidateIndex(row);
        ValidateIndex(column);
    }
    
    private static void ValidateIndex(int index)
    {
        if (index < 0)
            throw new IndexOutOfRangeException("Index must be greater than or equal to 0");
    }

    private static int ConvertToZeroBasedIndex(string index)
    {
        var result = 0;
        foreach (var c in index)
            if (char.IsLetter(c))
                result = result * 26 + (c - 'a');
            else
                throw new ArgumentException("Invalid character in index");
        return result;
    }
}