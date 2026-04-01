# Agent guide — Frank.GameEngine

Instructions for AI assistants (Cursor, Copilot, etc.) working in this repository.

## Product

Modular .NET game engine: core simulation and rendering abstractions with pluggable backends (Raylib, MonoGame, console). Libraries under `src/` are packable; `samples/` are executables; unit tests use **TUnit** on **Microsoft.Testing.Platform** (see `tests/README.md`).

## Build and verify

- **Solution file**: `Frank.GameEngine.slnx` (XML SLNX; migrate from `.sln` with `dotnet sln <path>.sln migrate` if needed).
- **SDK**: .NET 10 (`global.json` pins a minimum; `rollForward` allows newer feature bands).
- **`global.json`** includes `"test": { "runner": "Microsoft.Testing.Platform" }` so `dotnet test` uses the MTP experience required on .NET 10 SDK for TUnit.
- Restore and build:

```powershell
dotnet restore Frank.GameEngine.slnx
dotnet build Frank.GameEngine.slnx -c Release
dotnet test --solution Frank.GameEngine.slnx -c Release
```

- **Alternative**: `dotnet run --project tests/Frank.GameEngine.Tests/Frank.GameEngine.Tests.csproj -c Release` (TUnit test project is `OutputType` **Exe**). Coverage / TRX: add `--coverage`, `--report-trx` per [TUnit CLI](https://tunit.dev/docs/reference/command-line-flags/).
- **CI** (`.github/workflows/verify-dotnet.yml`) builds the **solution**, runs **`dotnet pack`** on shipping libraries, then **`dotnet test --project tests/Frank.GameEngine.Tests/Frank.GameEngine.Tests.csproj`** with **`TUNIT_DISABLE_GITHUB_REPORTER=true`** (and `DISABLE_GITHUB_REPORTER`). Packages land under **`artifacts/package/release/`** when packing locally (`dotnet pack Frank.GameEngine.slnx -c Release`).

- **Central Package Management**: versions live in `Directory.Packages.props`; project files use `PackageReference` without `Version`.
- **NuGet feeds**: repo `nuget.config` scopes restores to **nuget.org** with package source mapping (required for CPM when multiple feeds exist globally). To use another feed (e.g. Stride), add it under `packageSources` and map patterns under `packageSourceMapping`.

## Layout

| Area | Path |
|------|------|
| Core / physics / rendering contracts | `src/Frank.GameEngine.*` |
| Source generator (Roslyn) | `tools/Frank.GameEngine.Generators.AssetsGenerator` — **net10.0**, same `Microsoft.CodeAnalysis.CSharp` version as tests (central package file); `EnforceExtendedAnalyzerRules` is off with targeted `NoWarn` for legacy `ISourceGenerator` rules |
| Samples | `samples/*` — shared props in `samples/Directory.Build.props` |
| Tests | `tests/Frank.GameEngine.Tests` — TUnit + **FluentAssertions 7.x** (Apache 2.0; 8+ is Xceed-licensed and problematic for OSS CI) + Moq; folders `Core/`, `Physics/`, `Primitives/`, `Input/`, `Audio/`, `SubPrimitives/`, `Generators/`. Do **not** reference `Microsoft.NET.Test.Sdk` or coverlet — TUnit ships MTP coverage extensions. |
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

Do not commit secrets.

## CI and shipping

- **Local GitHub Actions**: `.github/workflows/verify-dotnet.yml` runs restore, build, and `dotnet test --solution Frank.GameEngine.slnx` on **ubuntu-latest** using the SDK from `global.json` (MTP / TUnit). Keep this in sync with how you run tests locally.
- **Reusable workflows** (`frankhaugen/Workflows`): PR/merge/release jobs still call those; update that repository if its `dotnet test` invocation predates `--solution` / MTP on .NET 10.
- **Debugging failed runs** (if `gh` is installed): `gh run list --workflow "Verify .NET"` then `gh run view <id> --log-failed`. Without `gh`, open the run in the browser or query `https://api.github.com/repos/frankhaugen/Frank.GameEngine/actions/runs?per_page=5` and the job’s `steps` for which step failed.
- **After work** (agents and humans): when build and tests pass locally, **commit** with a clear message and **push** the current branch unless the user asked otherwise. See `.cursor/rules/git-ship-after-verify.mdc`.
