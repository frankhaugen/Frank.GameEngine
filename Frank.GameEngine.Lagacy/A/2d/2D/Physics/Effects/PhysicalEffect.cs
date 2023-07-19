using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A._2d._2D.Physics.Effects;

public abstract class PhysicalEffect : IPhysicalEffect
{
	protected readonly EnvironmentalFactors _environment;

	protected PhysicalEffect(EnvironmentalFactors environment) => _environment = environment;

	public abstract Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}