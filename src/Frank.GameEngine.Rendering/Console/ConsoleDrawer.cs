using System.Numerics;
using System.Text;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Console;

public class ConsoleDrawer
{
    private readonly CharColor[,] _buffer;
    private readonly int _height;
    private readonly int _offsetX;
    private readonly int _offsetY;
    private readonly int _width;

    public ConsoleDrawer(int width, int height, int offsetX, int offsetY)
    {
        _offsetX = offsetX;
        _offsetY = offsetY;
        _width = width + Math.Abs(2 * offsetX);
        _height = height + Math.Abs(2 * offsetY);
        _buffer = new CharColor[_width, _height];
        Clear();
    }

    public ConsoleDrawer WithPixel(ConsolePixel pixel)
    {
        // Adjust the pixel coordinates to fit within the buffer
        int x = (int)pixel.Vector.X + _offsetX;
        int y = (int)pixel.Vector.Y + _offsetY;

        // Validate the indices for the buffer
        if (x < 0 || y < 0 || x >= _width || y >= _height)
        {
            throw new ArgumentOutOfRangeException($"Pixel coordinate ({pixel.Vector.X}, {pixel.Vector.Y}) is out of bounds. The valid range is x from {-_offsetX} to {_offsetX - 1}, y from {-_offsetY} to {_offsetY - 1}.");
        }

        _buffer[x, y] = pixel.CharColor;
        return this;
    }
    
    /// <summary>
    ///     Draws the points of the polygon to the buffer. This is useful for drawing the buffer to the console after all pixels have been set.
    /// </summary>
    /// <param name="polygon"></param>
    /// <param name="charColor"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public ConsoleDrawer WithPoints(Polygon polygon, CharColor charColor)
    {
        foreach (var point in polygon)
        {
            WithPixel(new ConsolePixel(point, charColor));
        }
        return this;
    }
    
    public ConsoleDrawer WithLines(Polygon polygon, CharColor charColor)
    {
        var edges = polygon.Edges;
        foreach (var edge in edges)
        {
            var points = edge.GetPoints();
        }
        return this;
    }

    /// <summary>
    ///    Draws the buffer to the console. This is useful for drawing the buffer to the console after all pixels have been set.
    ///    This uses the ConsolePixel class to draw each pixel. This is a slow method of drawing to the console and should be used sparingly.
    /// </summary>
    public void Draw()
    {
        for (var i0 = 0; i0 < _width; i0++)
        for (var i1 = 0; i1 < _height; i1++)
        {
            var pixel = new ConsolePixel(new Vector3(i0 - _offsetX, i1 - _offsetY, 0), _buffer[i0, i1]);
            pixel.Draw();
        }
    }
    
    /// <summary>
    ///    Clears the buffer. This is useful for clearing the buffer before drawing a new frame.
    /// </summary>
    public void Clear()
    {
        for (var i0 = 0; i0 < _width; i0++)
        for (var i1 = 0; i1 < _height; i1++)
            _buffer[i0, i1] = new CharColor(' ');
    }
    
    /// <summary>
    ///     Returns a string representation of the buffer. This is useful for debugging.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var i0 = 0; i0 < _width; i0++)
        {
            for (var i1 = 0; i1 < _height; i1++)
            {
                builder.Append(_buffer[i0, i1].Char);
            }
            builder.AppendLine();
        }
        return builder.ToString();
    }
}

public readonly struct ConsolePixel
{
    public Vector3 Vector { get; }
    public CharColor CharColor { get; }

    public ConsolePixel(Vector3 vector, CharColor charColor)
    {
        Vector = vector;
        CharColor = charColor;
    }

    public void Draw()
    {
        System.Console.SetCursorPosition((int)Vector.X, (int)Vector.Y);
        System.Console.ForegroundColor = CharColor.Color;
        System.Console.Write(CharColor.Char);
        System.Console.ResetColor();
    }

    public override string ToString()
    {
        return $"{Vector} {CharColor}";
    }
}

public readonly struct CharColor
{
    public char Char { get; }
    public ConsoleColor Color { get; }

    public CharColor(char @char, ConsoleColor color = ConsoleColor.White)
    {
        Char = @char;
        Color = color;
    }

    public override string ToString()
    {
        return $"{Char} {Color}";
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return (Char.GetHashCode() * 397) ^ (int)Color;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is CharColor charColor)
            return Char == charColor.Char && Color == charColor.Color;
        return false;
    }

    public static bool operator ==(CharColor left, CharColor right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(CharColor left, CharColor right)
    {
        return !(left == right);
    }
}