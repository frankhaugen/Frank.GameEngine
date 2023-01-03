﻿using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Physics;

public struct EnvironmentalFactors
{
	public float Gravity { get; set; }
	public Fluid Medium { get; set; }
	public Vector2 Wind { get; set; }

	public EnvironmentalFactors()
	{
		Wind = Vector2.Zero;
		Medium = new Fluid(FluidName.Vacuum);
		Gravity = 9.81f;
	}

	public EnvironmentalFactors(FluidName fluidName, float gravity)
	{
		Medium = new Fluid(fluidName);
		Gravity = gravity;
		Wind = Vector2.Zero;
	}
}