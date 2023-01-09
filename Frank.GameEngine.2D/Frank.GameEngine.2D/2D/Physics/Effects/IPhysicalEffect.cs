using Frank.GameEngine.Core._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core._2D.Physics.Effects;

public interface IPhysicalEffect
{
	Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}