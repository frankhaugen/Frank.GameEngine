using Frank.GameEngine.Lagacy._2d._2D.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy._2d._2D.Physics.Effects;

public class Wind : PhysicalEffect
{
	public Wind(EnvironmentalFactors environment) : base(environment)
	{
	}

	public override Vector2 Calculate(IGameObject gameObject, TimeSpan elapsedTime)
	{
		return _environment.Wind * (float)elapsedTime.TotalSeconds;
	}
}