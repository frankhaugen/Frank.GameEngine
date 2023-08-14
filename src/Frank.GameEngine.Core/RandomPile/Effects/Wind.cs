using System.Numerics;
using Frank.GameEngine.Primitives;

namespace Frank.GameEngine.Core.RandomPile.Effects;

public class Wind : PhysicalEffect
{
    public Wind(EnvironmentalFactors environment) : base(environment)
    {
    }

    public override Vector3 Calculate(GameObject gameObject, TimeSpan elapsedTime)
    {
        return _environment.Wind * (float)elapsedTime.TotalSeconds;
    }
}