﻿using Frank.GameEngine.Lagacy.A.OldCore.Physics;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

namespace Frank.GameEngine.Lagacy.A.OldCore.Services;

public class PhysicsService : IPhysicsService, IUpdateService
{
    private readonly PhysicalForces _forces;
    private readonly IOptions<WorldOptions> _worldOptions;
    private readonly GameObjects _gameObjects;

    public PhysicsService(PhysicalForces forces, IOptions<WorldOptions> worldOptions, GameObjects gameObjects)
    {
        _forces = forces;
        _worldOptions = worldOptions;
        _gameObjects = gameObjects;
    }
    
    public void Update(GameTime gameTime)
    {
        foreach (var gameObject in _gameObjects)
        {
            var totalForce = _forces.Aggregate(Vector3.Zero, (current, force) => current + force.GetForce(gameObject, gameTime, _worldOptions.Value));
        }
    }
}