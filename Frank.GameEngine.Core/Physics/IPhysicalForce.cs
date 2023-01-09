using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Physics;

public interface IPhysicalForce
{
    Vector3 GetForce(IGameObject gameObject, GameTime gameTime, WorldOptions worldOptions);
}