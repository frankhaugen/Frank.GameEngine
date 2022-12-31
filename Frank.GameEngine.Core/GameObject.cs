
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public class GameObject : IGameObject
{
    public GameObject()
    {

    }

    public GameObject(string name, bool physicsEnebled, bool collissionEnabled, float mass, Vector2 position, Vector2 velocity, Polygon polygon, Color color)
    {
        Name = name;
        PhysicsEnebled = physicsEnebled;
        CollissionEnabled = collissionEnabled;
        Mass = mass;
        Position = position;
        Velocity = velocity;
        Polygon = polygon;
        Color = color;
    }

    public string Name { get; set; }
    public bool PhysicsEnebled { get; set; }
    public bool CollissionEnabled { get; set; }
    public float Mass { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public Polygon Polygon { get; set; }
    public Color Color { get; set; }

}