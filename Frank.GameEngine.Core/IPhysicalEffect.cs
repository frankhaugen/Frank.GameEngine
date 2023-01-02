using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public interface IPhysicalEffect
{
    Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}