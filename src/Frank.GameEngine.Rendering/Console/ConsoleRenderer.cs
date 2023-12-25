using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Rendering.Console;

public class ConsoleRenderer : IRenderer
{
    private readonly Viewport _viewport;

    public ConsoleRenderer(int width, float aspectRatio, Vector3? offset = null)
    {
        var height = (int)(width / aspectRatio);
        _viewport = new Viewport(new Vector3(width, height, 0), offset);
    }

    public void Render(Scene scene)
    {
        
        var polygons = scene.GameObjects.Select(x => x.Shape.Polygon);
        RenderToViewport(polygons);
        System.Console.Clear();
        WriteBuffer(_viewport.GetBuffer());
    }
    
    public static void WriteBuffer(char[,] buffer)
    {
        var screenHeight = buffer.GetLength(0);
        var screenWidth = buffer.GetLength(1);
        
        var currentBuffer = new char[screenHeight, screenWidth];
        for (var i = 0; i < screenHeight; i++)
        for (var j = 0; j < screenWidth; j++)
            currentBuffer[i, j] = ' '; // Init with space
        
        if (currentBuffer is not null && buffer is not null)
        {
            for (int i = 0; i < Math.Min(currentBuffer.GetLength(0), buffer.GetLength(0)); i++)
            {
                for (int j = 0; j < Math.Min(currentBuffer.GetLength(1), buffer.GetLength(1)); j++)
                {
                    if (currentBuffer[i, j] == buffer[i, j]) continue;
                    if(j >= 0 && j < System.Console.BufferWidth && i >= 0 && i < System.Console.BufferHeight)
                    {
                        System.Console.SetCursorPosition(j, i);
                        System.Console.Write(buffer[i, j]);
                        currentBuffer[i, j] = buffer[i, j];
                    }
                }
            }
        }

        // for (var i = 0; i < screenHeight; i++)
        // {
        //     for (var j = 0; j < screenWidth; j++)
        //     {
                
                // if (currentBuffer[i, j] == buffer[i, j]) continue;
                // System.Console.SetCursorPosition(j, i);
                // System.Console.Write(buffer[i, j]);
                // currentBuffer[i, j] = buffer[i, j];
        //     }
        // }
    }

    private void RenderToViewport(IEnumerable<Polygon> polygons)
    {
        _viewport.Clear();

        foreach (var polygon in polygons)
        {
            DrawLines(polygon);
            DrawPolygon(polygon);
        }
    }

    private void DrawPolygon(Polygon polygon, string pixel = "#")
    {
        foreach (var vertex in polygon) _viewport.SetPixel(vertex, pixel);
    }

    private void DrawLines(Polygon polygon)
    {
        foreach (var edge in polygon.Edges)
        {
            var pixel = DetermineLinePixelFromAngle(edge);
            _viewport.DrawLine(edge, pixel);
        }
    }

    private string DetermineLinePixelFromAngle(Edge edge)
    {
        var angle = edge.GetAngle();
        if (angle >= 0 && angle < 45)
            return "─";
        if (angle >= 45 && angle < 90)
            return "╲";
        if (angle >= 90 && angle < 135)
            return "│";
        if (angle >= 135 && angle < 180)
            return "╱";
        if (angle >= 180 && angle < 225)
            return "─";
        if (angle >= 225 && angle < 270)
            return "╲";
        if (angle >= 270 && angle < 315)
            return "│";
        if (angle >= 315 && angle < 360)
            return "╱";

        return " ";
    }
}