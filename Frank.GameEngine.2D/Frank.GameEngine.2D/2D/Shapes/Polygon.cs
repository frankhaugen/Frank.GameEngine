using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Shapes;

	public struct Polygon
	{
		public Vector2[] Vertices { get; }

		public Polygon(params Vector2[] vertices) => Vertices = vertices;
	}