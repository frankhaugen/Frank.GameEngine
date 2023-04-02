using Frank.GameEngine.Lagacy._2d._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy._2d._2D.Physics.Effects;

public abstract class PhysicalEffect : IPhysicalEffect
{
	protected readonly EnvironmentalFactors _environment;

	protected PhysicalEffect(EnvironmentalFactors environment) => _environment = environment;

	public abstract Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime);
}