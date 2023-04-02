using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.OldCore.Shapes;

public struct Polygon3D
{
	public Vector3[] Vertices { get; }

	public Polygon3D(params Vector3[] vertices) => Vertices = vertices;
}

public struct Polygon2D
{
	public Vector2[] Vertices { get; }

	public Polygon2D(params Vector2[] vertices) => Vertices = vertices;
}