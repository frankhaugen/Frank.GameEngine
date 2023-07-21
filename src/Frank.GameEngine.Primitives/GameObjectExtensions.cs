using System.Numerics;

namespace Frank.GameEngine.Primitives;

public static class GameObjectExtensions
{
    /// <summary>
    /// Gets the shape of the game object after it has been transformed by the game object's transform.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static Shape GetTransformedShape(this GameObject gameObject)
    {
        var shape = gameObject.Shape;
        var transform = gameObject.Transform;
        var transformedShape = shape.GetTransformedShape(transform);
        return transformedShape;
    }
    
    /// <summary>
    /// Gets the collision between two game objects.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="otherGameObject"></param>
    /// <returns>
    /// Returns null if the game objects are not colliding. Otherwise, returns a collision object with the location of the collision, the two game objects involved in the collision, and the force of the collision.
    /// </returns>
    public static Collision? GetCollision(this GameObject gameObject, GameObject otherGameObject)
    {
        var transformedShape = gameObject.GetTransformedShape();
        var otherTransformedShape = otherGameObject.GetTransformedShape();
        var intersectionPoints = transformedShape.GetIntersectionPoints(otherTransformedShape).ToArray();
        var isIntersecting = intersectionPoints.Any();
        
        if (!isIntersecting)
            return null;
        
        var force = gameObject.Rigidbody.Velocity.GetMagnitude() + otherGameObject.Rigidbody.Velocity.GetMagnitude();
        var averageIntersectionPoint = intersectionPoints.Aggregate((a, b) => a + b) / intersectionPoints.Length;
        var normal = (gameObject.Transform.Position - otherGameObject.Transform.Position).GetNormal();
        
        var collision = new Collision(averageIntersectionPoint ,gameObject , otherGameObject, force, normal);
        
        return collision;
    }

    /// <summary>
    /// Gets the collisions between a game object and a collection of other game objects.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="otherGameObjects"></param>
    /// <returns></returns>
    public static IEnumerable<Collision> GetCollisions(this GameObject gameObject, IEnumerable<GameObject> otherGameObjects)
    {
        var collisions = new List<Collision>();
        
        Parallel.ForEach(otherGameObjects, otherGameObject =>
        {
            var collision = gameObject.GetCollision(otherGameObject);
            if (collision != null)
                collisions.Add(collision.Value);
        });
        
        return collisions;
    }

}