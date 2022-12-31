using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public static class PolygonFactory
{
    public static Polygon GetLine(Vector2 start, Vector2 end) => new Polygon(new[] { start, end });

    public static Polygon GetCircle(Vector2 center, int numSegments, float radius)
    {
        var vertices = new Vector2[numSegments];
        var angleStep = MathHelper.TwoPi / (float)numSegments;

        for (var i = 0; i < numSegments; i++)
        {
            var angle = i * angleStep;
            vertices[i] = center + radius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        return new Polygon(vertices);
    }

    public static Polygon GetTriangle(Vector2 position, float scale = 1f) =>
        new Polygon(new[]
        {
            position,
            position + new Vector2(0.5f * scale, -0.5f * scale),
            position + new Vector2(-0.5f * scale, -0.5f * scale),
        });

    public static Polygon GetSquare(Vector2 position, float scale = 1f) =>
        new Polygon(new[]
        {
            position + new Vector2(-0.5f * scale, -0.5f * scale),
            position + new Vector2(0.5f * scale, -0.5f * scale),
            position + new Vector2(0.5f * scale, 0.5f * scale),
            position + new Vector2(-0.5f * scale, 0.5f * scale),
        });

    public static Polygon GetPentagon(Vector2 position, float scale = 1f) =>
        new Polygon(new[]
        {
            position + new Vector2(-0.5f * scale, -0.5f * scale),
            position + new Vector2(-0.1f * scale, -0.5f * scale),
            position + new Vector2(0.5f * scale, 0f * scale),
            position + new Vector2(-0.1f * scale, 0.5f * scale),
            position + new Vector2(-0.5f * scale, 0.5f * scale),
        });

    public static Polygon GetArtilleryShellPolygon(Vector2 position, float scale = 1f)
    {
        var vertices = new Vector2[]
        {
            new Vector2(-0.155f * scale / 2, 0),
            new Vector2(-0.155f * scale / 2, -0.640f * scale / 3),
            new Vector2(0.155f * scale / 2, -0.640f * scale / 3),
            new Vector2(0.155f * scale / 2, 0),
            new Vector2(0, 0.640f * scale / 3)
        };

        var polygon = new Polygon(vertices);
        return polygon.Translate(position);
    }


}