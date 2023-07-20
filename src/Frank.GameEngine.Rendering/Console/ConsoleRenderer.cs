using Frank.GameEngine.Primitives;
using System.Numerics;

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
        Render(scene, x =>
        {
            System.Console.Clear();
            System.Console.WriteLine(x);
        });
    }

    public void Render(Scene scene, Action<string> callback)
    {
        var polygons = scene.GetTransformedShapes().Select(x => x.Polygon);
        var consoleImage = Render(polygons);
        callback(consoleImage);
    }

    private string Render(IEnumerable<Polygon> polygons)
    {
        _viewport.Clear();

        foreach (var polygon in polygons)
        {
            DrawLines(polygon);
            DrawPolygon(polygon);
        }

        return _viewport.ToString();
    }

    private void DrawPolygon(Polygon polygon, string pixel = "#")
    {
        foreach (var vertex in polygon)
        {
            _viewport.SetPixel(vertex, pixel);
        }
    }

    private void DrawLines(Polygon polygon)
    {
        foreach (var edge in polygon.Edges)
        {
            var pixel = DetermineLinePixelFromAngle(edge);
            _viewport.DrawLine(edge, pixel);
        }
    }
    private void DrawLinesV1(Polygon polygon)
    {
        for (var i = 0; i < polygon.Length - 1; i++)
        {
            var edge = new Edge(polygon[i], polygon[i + 1]);
            var pixel = DetermineLinePixelFromAngle(edge);
            _viewport.DrawLine(edge, pixel);
        }
        
        // Draw line from last vertex to first
        var finaleEdge = new Edge(polygon[^1], polygon[0]);
        var finalPixel = DetermineLinePixelFromAngle(finaleEdge);
        _viewport.DrawLine(finaleEdge, finalPixel);
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