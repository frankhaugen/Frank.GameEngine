using Frank.GameEngine.Core.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Physics.Effects;

public interface IPhysicalEffect
{
	Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}