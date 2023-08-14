using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Core.RandomPile.Effects;

public interface IPhysicalEffect
{
    Vector3 Calculate(GameObject gameObject, TimeSpan elapsedTime);
}