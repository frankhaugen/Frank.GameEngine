using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy._2d._2D.Shapes;

	public struct Polygon
	{
		public Vector2[] Vertices { get; }

		public Polygon(params Vector2[] vertices) => Vertices = vertices;
	}