using Frank.GameEngine.Collections;
using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine;

public class GameObjectFactory
{
    public static GameObject CreateTestSquare()
    {
        var name = "TestSquare";
        
        var transform = new Transform()
        {
            Position = Vector3.Zero,
            Rotation = Quaternion.Identity,
            Scale = Vector3.One
        };
        
        var options = new GameObjectOptions()
        {
            IsVisible = true,
            IsCollidable = true,
            IsPhysical = true
        };
        
        var physicalProperties = new PhysicalProperties()
        {
            Mass = 10,
            Velocity = Vector3.Zero,
        };
        
        var polygons = new Polygons();
        var polygon = PolygonFactory.CreateSquare(Vector2.Zero, 1, 1);
        polygons.Add(polygon);
        
        return new GameObject(name, transform, polygons, options, physicalProperties);
    }
}