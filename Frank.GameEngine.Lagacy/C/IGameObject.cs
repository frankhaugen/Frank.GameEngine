using Frank.GameEngine.Collections;
using Frank.GameEngine.Types;

namespace Frank.GameEngine;

public interface IGameObject : IUniqueIdentifier
{
    string Name { get; set; }
    Transform Transform { get; }
    Polygons Polygons { get; set; }
    public PhysicalProperties PhysicalProperties { get; set; }
    GameObjectOptions Options { get; }
}