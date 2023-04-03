using Frank.GameEngine.Collections;
using Frank.GameEngine.Types;

namespace Frank.GameEngine;

public class GameObject : IUniqueIdentifier
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public Transform Transform { get; set; }
    public Polygons Polygons { get; set; }
    public GameObjectOptions Options { get; set; }
}