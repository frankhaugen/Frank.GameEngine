using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public interface IGameObjectBase
{
    public string Name { get; set; }
    public bool PhysicsEnebled { get; set; }
    public bool CollissionEnabled { get; set; }

    public float Mass { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Polygon Polygon { get; set; }
    public Color Color { get; set; }
}