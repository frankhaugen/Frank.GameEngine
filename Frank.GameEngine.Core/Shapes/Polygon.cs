using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Shapes;

public struct Polygon
{
	public Vector2[] Vertices { get; set; }

	public Polygon(params Vector2[] vertices) => Vertices = vertices;
}