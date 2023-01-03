using Frank.GameEngine.Core.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Frank.GameEngine.Core.GameObjects;

public interface IGameObjectBase
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