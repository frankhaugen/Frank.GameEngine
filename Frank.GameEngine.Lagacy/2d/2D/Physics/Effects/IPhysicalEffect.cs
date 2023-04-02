using Frank.GameEngine.Lagacy._2d._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy._2d._2D.Physics.Effects;

public interface IPhysicalEffect
{
	Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}