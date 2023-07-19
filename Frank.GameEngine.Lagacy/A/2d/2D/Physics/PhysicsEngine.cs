using Frank.GameEngine.Lagacy.A._2d._2D.Physics.Effects;

namespace Frank.GameEngine.Lagacy.A._2d._2D.Physics;

public class PhysicsEngine : IPhysicsEngine
{
	private readonly EnvironmentalFactors _environment;
	private readonly List<IPhysicalEffect> _physicalEffects = new();

	public PhysicsEngine(EnvironmentalFactors environment)
	{
		_environment = environment;
		_physicalEffects.Add(new Gravity(_environment));
		// _physicalEffects.Add(new Drag(_environment));
		// _physicalEffects.Add(new Lift(_environment));
	}

	public void Update(IGameObject gameObject, TimeSpan elapsed)
	{
		if (!gameObject.PhysicsEnebled) return;
		foreach (var physicalEffect in _physicalEffects)
		{
			gameObject.Velocity += physicalEffect.Calculate(gameObject, elapsed);
		}

		gameObject.Position += gameObject.Velocity;
	}
}