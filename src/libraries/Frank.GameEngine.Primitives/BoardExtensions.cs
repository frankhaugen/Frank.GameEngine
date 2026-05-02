namespace Frank.GameEngine.Primitives;

public static class BoardExtensions
{
    public static T Get<T>(this T[,] array, BoardPosition position)
    {
        return array[position.Row - 1, position.Column - 1];
    }
}