using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A._2d._2D.Physics.Effects;

public interface IPhysicalEffect
{
	Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}