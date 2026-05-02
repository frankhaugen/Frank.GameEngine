# Samples

Executable demos under `samples/Frank.GameEngine.Samples.*`. Each project sets `IsPackable` to `false` and references engine libraries as needed (see [Directory.Build.props](Directory.Build.props) for shared sample build settings).

## Run a single sample

From the repository root:

```powershell
dotnet run --project samples/Frank.GameEngine.Samples.Pong/Frank.GameEngine.Samples.Pong.csproj -c Release
```

Swap the project path for Battleship, BouncingBall, Fps, or Hello2D.

## .NET Aspire AppHost (all samples, local dev)

The **Aspire AppHost** lives under [`dev/Frank.GameEngine.Samples.AppHost`](../dev/Frank.GameEngine.Samples.AppHost/) so it does not inherit the shared `ProjectReference` block from this folder’s `Directory.Build.props`.

```powershell
dotnet run --project dev/Frank.GameEngine.Samples.AppHost/Frank.GameEngine.Samples.AppHost.csproj
```

Use the dashboard to start **one** windowed sample at a time (Raylib/MonoGame and multiple GL windows do not mix well). Continuous integration only **compiles** the AppHost as part of the solution; it does not launch interactive windows on the build agents.

## Learn more

- [src/libraries/README.md](../src/libraries/README.md) — shipping NuGet projects referenced by samples.
- [AGENTS.md](../AGENTS.md) — build, platforms, and agent guidance.
- [docs/architecture.md](../docs/architecture.md) — execution models and renderer choice.
- [docs/evaluation-major.md](../docs/evaluation-major.md) — how Aspire fits next to product priorities.
