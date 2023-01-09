using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Shapes;

public struct Polygon3D
{
	public Vector3[] Vertices { get; }

	public Polygon3D(params Vector3[] vertices) => Vertices = vertices;
}