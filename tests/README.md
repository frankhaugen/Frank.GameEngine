# Tests

## Unit tests (`Frank.GameEngine.Tests`)

- **Framework**: [TUnit](https://tunit.dev/) on **Microsoft.Testing.Platform** (not VSTest). The project is an executable (`OutputType` = `Exe`).
- **Assertions**: FluentAssertions (`.Should()`) alongside TUnit’s `[Test]` / `[Arguments]`.
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

CI builds the test project then runs `dotnet run --project tests/Frank.GameEngine.Tests/Frank.GameEngine.Tests.csproj` (TUnit host); `dotnet test` + MTP failed on Actions for this repo.

Or run the test project directly:

```powershell
dotnet run --project tests/Frank.GameEngine.Tests/Frank.GameEngine.Tests.csproj -c Release
```

Optional: `--coverage`, `--report-trx` on `dotnet run` (see TUnit docs).

## Other projects under `tests/`

- **`Frank.GameEngine.Tests.Benchmarks`** — BenchmarkDotNet (not TUnit).
- **`Frank.GameEngine.Tests.Application`** — hosted worker for manual / integration experiments, not the unit test suite.
