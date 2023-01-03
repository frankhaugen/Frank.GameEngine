﻿using Frank.GameEngine.Core.GameObjects;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Core.Physics.Effects;

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