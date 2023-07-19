using Frank.GameEngine.Collections;
using Frank.GameEngine.Types;

namespace Frank.GameEngine;

public class GameObject : IGameObject
{
    public GameObject(string name, Transform transform, Polygons polygons, GameObjectOptions options, PhysicalProperties physicalProperties)
    {
        Name = name;
        Transform = transform;
        Polygons = polygons;
        Options = options;
        PhysicalProperties = physicalProperties;
    }
    
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public Transform Transform { get; }
    public Polygons Polygons { get; set; }
    public PhysicalProperties PhysicalProperties { get; set; }
    public GameObjectOptions Options { get; }
}