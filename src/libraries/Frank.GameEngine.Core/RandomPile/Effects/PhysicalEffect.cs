using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Core.RandomPile.Effects;

public abstract class PhysicalEffect : IPhysicalEffect
{
    protected readonly EnvironmentalFactors _environment;

    protected PhysicalEffect(EnvironmentalFactors environment)
    {
        _environment = environment;
    }

    public abstract Vector3 Calculate(GameObject gameObject, TimeSpan elapsedTime);
}