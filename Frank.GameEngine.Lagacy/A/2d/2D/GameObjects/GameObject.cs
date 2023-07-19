using Frank.GameEngine.Lagacy.A._2d._2D.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Lagacy.A._2d._2D.GameObjects;

public class GameObject : IGameObject
{
	public string Name { get; set; }
	public bool PhysicsEnebled { get; set; }
	public bool CollissionEnabled { get; set; }
	public float Mass { get; set; }
	public Vector2 Position { get; set; }
	public Vector2 Velocity { get; set; }
	public Vector2 Direction { get; set; }
	public Polygon Polygon { get; set; }
	public Color Color { get; set; }
	public Texture2D? Texture { get; set; }
}