# Frank.GameEngine documentation

## Guides

- [Architecture](architecture.md) — project layers, IRenderer, and the two runtime models.
- [Critical improvements](critical-improvements.md) — design fixes applied and follow-up backlog.

## Quick orientation

- Simulation physics: Frank.GameEngine.Physics.PhysicsEngine.
- Raylib plus DI experiment: Frank.GameEngine.Rendering.RayLib — RayLibHostedPhysicsService, AddRayLibHostedPhysics, channels and RenderQueue.
- Samples: use PhysicsEngine plus GameEngine plus Simulator unless hosting the Raylib worker.

## Contributing

See CONTRIBUTING.md and AGENTS.md in the repository root.

## License

MIT — see LICENSE in the repository root.
