using Frank.GameEngine.Primitives;
using System.Numerics;

namespace Frank.GameEngine.Core.RandomPile.Effects;

public interface IPhysicalEffect
{
	Vector3 Calculate(GameObject gameObject, TimeSpan elapsedTime);
}