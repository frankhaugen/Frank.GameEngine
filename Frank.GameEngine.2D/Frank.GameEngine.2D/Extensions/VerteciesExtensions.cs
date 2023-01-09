using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Frank.GameEngine._2D.Models.BasicShapes;

namespace Frank.GameEngine._2D.Extensions;

internal static class VerteciesExtensions
{
    //internal static void AddTriange(this Vertices source) => source.;


    internal static IReadOnlyList<Vector2> GetVector2s(this Vertices source) => source.GetVector2s();
    internal static IReadOnlyList<Vector3> GetVector3s(this Vertices source) => source.GetVector3s();

    internal static IReadOnlyList<Vector2> GetVector2s(this IEnumerable<VertexPositionColor> source) => source.Select(x => x.Position.ToVector2()).ToList();
    internal static IReadOnlyList<Vector3> GetVector3s(this IEnumerable<VertexPositionColor> source) => source.Select(x => x.Position).ToList();
}

public class MyClass
{
    public void DrawLine(Vector2 a, Vector2 b, Color color)
    {
        this.DrawLine(a.X, a.Y, b.X, b.Y, color);
    }

    public void DrawLine(float x1, float y1, float x2, float y2, Color color)
    {
        // Default thickness with no zoom.
        var thickness = 2f;


        var halfThickness = thickness * 0.5f;

        // Line edge pointing from "b" to "a".
        var e1x = x2 - x1;
        var e1y = y2 - y1;

        Normalize(ref e1x, ref e1y);

        var n1x = -e1y;
        var n1y = e1x;

        e1x *= halfThickness;
        e1y *= halfThickness;

        n1x *= halfThickness;
        n1y *= halfThickness;

        var e2x = -e1x;
        var e2y = -e1y;

        var n2x = -n1x;
        var n2y = -n1y;

        var qax = x1 + n1x + e2x;
        var qay = y1 + n1y + e2y;

        var qbx = x2 + n1x + e1x;
        var qby = y2 + n1y + e1y;

        var qcx = x2 + n2x + e1x;
        var qcy = y2 + n2y + e1y;

        var qdx = x1 + n2x + e2x;
        var qdy = y1 + n2y + e2y;

        this.DrawQuadFill(qax, qay, qbx, qby, qcx, qcy, qdx, qdy, color);
    }

    public static void Normalize(ref float x, ref float y)
    {
        var invLen = 1f / MathF.Sqrt(x * x + y * y);
        x *= invLen;
        y *= invLen;
    }

    internal void DrawQuadFill(float ax, float ay, float bx, float by, float cx, float cy, float dx, float dy, Color color)
    {
        const int shapeVertexCount = 4;
        const int shapeIndexCount = 6;

        //source.Indicies[source.IndexCount++] = 0 + source.VertexCount;
        //source.Indicies[source.IndexCount++] = 1 + source.VertexCount;
        //source.Indicies[source.IndexCount++] = 2 + source.VertexCount;
        //source.Indicies[source.IndexCount++] = 0 + source.VertexCount;
        //source.Indicies[source.IndexCount++] = 2 + source.VertexCount;
        //source.Indicies[source.IndexCount++] = 3 + source.VertexCount;

        //source.vertices[source.VertexCount++] = new VertexPositionColor(new Vector3(ax, ay, 0f), color);
        //source.vertices[source.VertexCount++] = new VertexPositionColor(new Vector3(bx, by, 0f), color);
        //source.vertices[source.VertexCount++] = new VertexPositionColor(new Vector3(cx, cy, 0f), color);
        //source.vertices[source.VertexCount++] = new VertexPositionColor(new Vector3(dx, dy, 0f), color);

        //source.shapeCount++;
    }

    public void DrawQuadFill(Vector2 a, Vector2 b, Vector2 c, Vector2 d, Color color)
    {
        DrawQuadFill(a.X, a.Y, b.X, b.Y, c.X, c.Y, d.X, d.Y, color);
    }
}