using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core;

public abstract class PhysicalEffect : IPhysicalEffect
{
    protected readonly EnvironmentalFactors _environment;

    protected PhysicalEffect(EnvironmentalFactors environment) => _environment = environment;

    public abstract Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}