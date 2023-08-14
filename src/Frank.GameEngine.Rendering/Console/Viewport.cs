using System.Numerics;
using System.Text;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Console;

public class Viewport
{
    private readonly string[,] _buffer;
    private readonly Vector3 _offset;

    public Viewport(Vector3 size, Vector3? offset = null)
    {
        _offset = offset ?? Vector3.Zero;
        _buffer = new string[(int)size.X + 2, (int)size.Y + 2];

        for (var y = 0; y < size.Y; y++)
        for (var x = 0; x < size.X; x++)
            _buffer[x, y] = " ";

        CreateBorder();
    }

    private void CreateBorder()
    {
        for (var x = 0; x < _buffer.GetLength(0); x++)
        {
            _buffer[x, 0] = "─";
            _buffer[x, _buffer.GetLength(1) - 1] = "─";
        }

        for (var y = 0; y < _buffer.GetLength(1); y++)
        {
            _buffer[0, y] = "│";
            _buffer[_buffer.GetLength(0) - 1, y] = "│";
        }

        _buffer[0, 0] = "┌";
        _buffer[_buffer.GetLength(0) - 1, 0] = "┐";
        _buffer[0, _buffer.GetLength(1) - 1] = "└";
        _buffer[_buffer.GetLength(0) - 1, _buffer.GetLength(1) - 1] = "┘";

        var horizontalSpacing = (_buffer.GetLength(0) - 2) / 10;
        var verticalSpacing = (_buffer.GetLength(1) - 2) / 10;

        for (var i = 1; i <= 10; i++)
        {
            var x = i * horizontalSpacing;
            _buffer[x, 0] = "┬";
            _buffer[x, _buffer.GetLength(1) - 1] = "┴";

            var y = i * verticalSpacing;
            _buffer[0, y] = "├";
            _buffer[_buffer.GetLength(0) - 1, y] = "┤";
        }

        _buffer[1, _buffer.GetLength(1) - 2] = "+";
    }

    public void Clear()
    {
        for (var y = 0; y < _buffer.GetLength(1); y++)
        for (var x = 0; x < _buffer.GetLength(0); x++)
            _buffer[x, y] = " ";

        CreateBorder();
    }

    public void DrawLine(Edge edge, string pixel)
    {
        var x1 = (int)(edge.A.X - _offset.X);
        var y1 = (int)(edge.A.Y - _offset.Y);

        var x2 = (int)(edge.B.X - _offset.X);
        var y2 = (int)(edge.B.Y - _offset.Y);

        var dx = Math.Abs(x2 - x1);
        var dy = Math.Abs(y2 - y1);

        var sx = x1 < x2 ? 1 : -1;
        var sy = y1 < y2 ? 1 : -1;

        var err = dx - dy;

        while (true)
        {
            SetPixel(new Vector3(x1, y1, 0), pixel);

            if (x1 == x2 && y1 == y2) break;

            var e2 = 2 * err;

            if (e2 > -dy)
            {
                err = err - dy;
                x1 = x1 + sx;
            }

            if (e2 < dx)
            {
                err = err + dx;
                y1 = y1 + sy;
            }
        }
    }

    public void SetPixel(Vector3 vector, string pixel)
    {
        var x = (int)(vector.X - _offset.X);
        var y = (int)(vector.Y - _offset.Y);

        if (x >= 0 && x < _buffer.GetLength(0) && y >= 0 && y < _buffer.GetLength(1))
            _buffer[x, _buffer.GetLength(1) - y - 1] = pixel;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        for (var y = 0; y < _buffer.GetLength(1); y++)
        {
            for (var x = 0; x < _buffer.GetLength(0); x++) builder.Append(_buffer[x, y]);

            builder.AppendLine();
        }

        return builder.ToString();
    }
}