# Frank.GameEngine documentation

## Guides

- [Architecture](architecture.md) — project layers, IRenderer, and the two runtime models.
- [Critical improvements](critical-improvements.md) — design fixes applied and follow-up backlog.
- [Evaluation — major (executive)](evaluation-major.md) — synthesized priorities across product, engineering, samples, and CI.
- [Evaluation — product and roadmap](evaluation-product-and-roadmap.md) — maturity, positioning, and strategic recommendations.
- [Evaluation — technical improvements](evaluation-technical-improvements.md) — concrete backlog aligned with the codebase.

## Quick orientation

- Shipping packages (NuGet): projects under `src/libraries/` — see [src/libraries/README.md](../src/libraries/README.md).
- Simulation physics: Frank.GameEngine.Physics.PhysicsEngine.
- Raylib plus DI experiment: Frank.GameEngine.Rendering.RayLib — RayLibHostedPhysicsService, AddRayLibHostedPhysics, channels and RenderQueue.
- Samples: use PhysicsEngine plus GameEngine plus Simulator unless hosting the Raylib worker.

## Tests

Unit tests live under `tests/Frank.GameEngine.Tests` (TUnit). See [tests/README.md](../tests/README.md) for layout and how to run them (`dotnet test --solution Frank.GameEngine.slnx`).

## Contributing

See CONTRIBUTING.md and AGENTS.md in the repository root.

## License

MIT — see LICENSE in the repository root.
