using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A._2d._2D.Shapes;

public static class PolygonFactory
{
	public static Polygon GetLine(Vector2 start, Vector2 end) => new Polygon(new[] { start, end });

	public static Polygon GetCircle(int numSegments, float radius)
	{
		var vertices = new Vector2[numSegments];
		var angleStep = MathHelper.TwoPi / numSegments;

		for (var i = 0; i < numSegments; i++)
		{
			var angle = i * angleStep;
			vertices[i] = Vector2.Zero + radius * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
		}

		return new Polygon(vertices);
	}

	public static Polygon GetTriangle(float scale = 1f) =>
		new Polygon(new[]
		{
			Vector2.Zero,
			Vector2.Zero + new Vector2(0.5f * scale, -0.5f * scale),
			Vector2.Zero + new Vector2(-0.5f * scale, -0.5f * scale),
		});

	public static Polygon GetSquare(float scale = 1f) =>
		new Polygon(new[]
		{
			Vector2.Zero + new Vector2(-0.5f * scale, -0.5f * scale),
			Vector2.Zero + new Vector2(0.5f * scale, -0.5f * scale),
			Vector2.Zero + new Vector2(0.5f * scale, 0.5f * scale),
			Vector2.Zero + new Vector2(-0.5f * scale, 0.5f * scale),
		});

	public static Polygon GetPentagon(float scale = 1f) =>
		new Polygon(new[]
		{
			Vector2.Zero + new Vector2(-0.5f * scale, -0.5f * scale),
			Vector2.Zero + new Vector2(-0.1f * scale, -0.5f * scale),
			Vector2.Zero + new Vector2(0.5f * scale, 0f * scale),
			Vector2.Zero + new Vector2(-0.1f * scale, 0.5f * scale),
			Vector2.Zero + new Vector2(-0.5f * scale, 0.5f * scale),
		});

	public static Polygon GetArtilleryShellPolygon(float scale = 1f)
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
		return polygon;
	}
}