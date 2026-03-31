# Tests

## Unit tests (`Frank.GameEngine.Tests`)

- **Framework**: [TUnit](https://tunit.dev/) on **Microsoft.Testing.Platform** (not VSTest). The project is an executable (`OutputType` = `Exe`).
- **Assertions**: FluentAssertions **7.x** (`.Should()`); version is pinned in `Directory.Packages.props` (8+ uses a license model that is a poor fit for default OSS CI).
- **Layout** (mirror engine areas):
  - `Core/` — `GameEngine`, `Simulator`, `SceneManager`, `UpdateArgs`, etc.
  - `Physics/` — engine, forces, integration-style cases.
  - `Physics/Forces/` — per-force unit tests.
  - `Primitives/` — boards, scenes, polygons, transforms.
  - `SubPrimitives/` — `Array2D` and related types.
  - `Input/` — key converters.
  - `Audio/` — audio helpers.
  - `Generators/` — Roslyn generator and predictable GUID smoke / diagnostic tests.

**Run from repo root** (requires `global.json` `test.runner` = `Microsoft.Testing.Platform`):

```powershell
dotnet test --solution Frank.GameEngine.slnx -c Release
```

CI builds the test project then runs `dotnet run --project tests/Frank.GameEngine.Tests/Frank.GameEngine.Tests.csproj` with `TUNIT_DISABLE_GITHUB_REPORTER=true` (avoids a bad exit code from the GitHub step-summary reporter on Actions).

Or run the test project directly:

```powershell
dotnet run --project tests/Frank.GameEngine.Tests/Frank.GameEngine.Tests.csproj -c Release
```

Optional: `--coverage`, `--report-trx` on `dotnet run` (see TUnit docs).

## Other projects under `tests/`

- **`Frank.GameEngine.Tests.Benchmarks`** — BenchmarkDotNet (not TUnit).
- **`Frank.GameEngine.Tests.Application`** — hosted worker for manual / integration experiments, not the unit test suite.
