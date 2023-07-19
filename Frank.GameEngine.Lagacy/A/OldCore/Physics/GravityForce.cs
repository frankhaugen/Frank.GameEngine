﻿using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore.Physics;

public class GravityForce : IPhysicalForce
{
    public Vector3 GetForce(IGameObject gameObject, GameTime gameTime, WorldOptions worldOptions)
    {
        return worldOptions.Gravity;
    }
}