# Agent guide — Frank.GameEngine

Instructions for AI assistants (Cursor, Copilot, etc.) working in this repository.

## Product

Modular .NET game engine: core simulation and rendering abstractions with pluggable backends (Raylib, MonoGame, console). Libraries under `src/` are packable; `samples/` are executables; `tests/` are xUnit.

## Build and verify

- **Solution file**: `Frank.GameEngine.slnx` (XML SLNX; migrate from `.sln` with `dotnet sln <path>.sln migrate` if needed).
- **SDK**: .NET 10 (`global.json` pins a minimum; `rollForward` allows newer feature bands).
- Restore and build:

```powershell
dotnet restore Frank.GameEngine.slnx
dotnet build Frank.GameEngine.slnx -c Release
dotnet test Frank.GameEngine.slnx -c Release
```

- **Central Package Management**: versions live in `Directory.Packages.props`; project files use `PackageReference` without `Version`.
- **NuGet feeds**: repo `nuget.config` scopes restores to **nuget.org** with package source mapping (required for CPM when multiple feeds exist globally). To use another feed (e.g. Stride), add it under `packageSources` and map patterns under `packageSourceMapping`.

## Layout

| Area | Path |
|------|------|
| Core / physics / rendering contracts | `src/Frank.GameEngine.*` |
| Source generator (Roslyn) | `tools/Frank.GameEngine.Generators.AssetsGenerator` — **net10.0**, same `Microsoft.CodeAnalysis.CSharp` version as tests (central package file); `EnforceExtendedAnalyzerRules` is off with targeted `NoWarn` for legacy `ISourceGenerator` rules |
| Samples | `samples/*` — shared props in `samples/Directory.Build.props` |
| Tests | `tests/Frank.GameEngine.Tests` — xUnit + FluentAssertions + Moq; grouped under `Core/`, `Physics/`, `Primitives/`, `Input/`, `Audio/` |
| Design docs | `docs/architecture.md`, `docs/critical-improvements.md` |

## Conventions

- **Target framework**: **net10.0** for every project, including the Roslyn generator tool.
- **Quality**: `TreatWarningsAsErrors`, nullable reference types, XML docs on public API (CS1591 suppressed globally).
- **Platform**: `ConsoleAudioPlayer` / `AudioPlayerFactory.CreateConsoleAudioPlayer` are **Windows-only** (`SupportedOSPlatform`). Samples use `SilentAudioPlayer` on other OSes. Respect CA1416 when adding platform APIs.

## Delegation (subagents / tasks)

Use focused passes when work is broad or orthogonal:

1. **Explore**: map folders, find implementations, list callers — before large refactors.
2. **Shell**: restore, build, test, `dotnet format`, package queries — run locally and report output.
3. **General**: multi-step features touching many projects.

In Cursor, prefer the Task/agent tools for wide searches; keep edits minimal and aligned with existing style (see `.cursor/rules/`).

## Cursor artifacts

- `.cursor/rules/*.mdc` — project rules (always-on + path globs).
- `.cursor/agents/*.md` — short role prompts you can paste or attach for specialized turns.

Do not commit secrets. CI may use reusable workflows under `frankhaugen/Workflows`; ensure any solution path there points at `Frank.GameEngine.slnx` if the workflow supports it.
