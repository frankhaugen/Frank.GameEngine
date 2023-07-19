using System.Numerics;

namespace Frank.GameEngine.Core.RandomPile;

public struct EnvironmentalFactors
{
	public float Gravity { get; set; }
	public Fluid Medium { get; set; }
	public Vector3 Wind { get; set; }

	public EnvironmentalFactors()
	{
		Wind = Vector3.Zero;
		Medium = new Fluid(FluidName.Vacuum);
		Gravity = -9.81f;
	}

	public EnvironmentalFactors(FluidName fluidName, float gravity = -9.81f)
	{
		Medium = new Fluid(fluidName);
		Gravity = gravity;
		Wind = Vector3.Zero;
	}
}