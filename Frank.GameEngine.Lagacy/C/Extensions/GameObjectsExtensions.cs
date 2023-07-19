using Frank.GameEngine.Rendering;
using Frank.GameEngine.Types;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Extensions;

public static class GameObjectsExtensions
{
    public static IRenderQueue GetRenderQueue(this IEnumerable<GameObject> gameObjects)
    {
        var renderQueue = new RenderQueue();
        foreach (var polygon in gameObjects.SelectMany(gameObject => gameObject.Polygons))
        {
            renderQueue.Enqueue(polygon);
        }

        return renderQueue;
    }

    public static void Update(this IEnumerable<GameObject> gameObjects, GameTime gameTime)
    {
        foreach (var gameObject in gameObjects)
        {
            ApplyFriction(gameObject);
            ApplyGravity(gameObject);
            ApplyVelocity(gameObject, gameTime);
        }
    }
    
    public static IEnumerable<IPolygon> GetPolygons(this IEnumerable<GameObject> gameObjects) => gameObjects.SelectMany(gameObject => gameObject.Polygons);
    
    private static void ApplyFriction(GameObject gameObject)
    {
        var friction = 0.01f;
        var frictionForce = gameObject.PhysicalProperties.Velocity * friction;
        gameObject.PhysicalProperties.Velocity -= frictionForce;
    }
    
    private static void ApplyGravity(GameObject gameObject)
    {
        var gravity = 0.01f;
        var gravityForce = new Vector3(0, -gravity, 0);
        gameObject.PhysicalProperties.Velocity += gravityForce;
    }
    
    private static void ApplyVelocity(GameObject gameObject, GameTime gameTime)
    {
        gameObject.Transform.Position += gameObject.PhysicalProperties.Velocity * gameTime.ElapsedGameTime.Milliseconds;
    }
}