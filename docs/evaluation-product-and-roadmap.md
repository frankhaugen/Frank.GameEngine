# Frank.GameEngine — product evaluation

This document evaluates the engine as a **modular .NET library product** (not a full editor or “Unity competitor”). It complements [architecture.md](architecture.md) and [critical-improvements.md](critical-improvements.md).

## What the engine is today

Frank.GameEngine is best described as a **small, testable simulation and rendering toolkit** for .NET 10:

- **Primitives** define scenes, cameras, meshes, and simple 2D objects.
- **Rendering** is abstracted behind `IRenderer` / `IRenderer2D` with multiple backends (Raylib and MonoGame are the interactive ones most users will care about).
- **Physics** is a thin integration layer over primitives with pluggable forces and collision hooks—not a full rigid-body solver.
- **Core** composes physics, input, audio, and scene management into `GameEngine` / `GameEngine2D`.

Strengths of this positioning:

- **Clear seams** for swapping renderers or hosting models.
- **Modern .NET** (net10.0), CPM, and a CI story that builds, packs, and tests.
- **Honest scope**: the docs already call out limitations (e.g. polygon fan triangulation, mesh collision approximations).

## Maturity assessment

| Area | Maturity | Notes |
|------|----------|--------|
| Build / packaging | Strong | Versioned NuGet metadata, `dotnet pack`, tests on MTP/TUnit. |
| 3D rendering (basic) | Moderate | Raylib + MonoGame paths exist; Assimp pipeline for assets. |
| 2D rendering | Early | Solid-color quads; textures called out as future in architecture. |
| Physics / collision | Early | Useful for demos; not a complete game physics stack. |
| Input / audio | Moderate | `IInputSource` is a good abstraction; platform audio split is documented. |
| Samples | Mixed | Pong/BouncingBall show 3D + Simulator; Battleship/Hello2D show 2D; patterns differ. |
| Hosted / DI pipeline (Raylib) | Experimental | Parallel to core loop; naming was improved but two “ways to run” remain. |

Overall: **viable for learning, prototypes, and small games** that fit the existing primitives. It is **not** yet a turnkey “drop in and ship a commercial title” engine without significant game-specific code.

## Gaps relative to typical “game engine” expectations

Many developers expect, out of the box:

1. **Sprites and atlases** — bitmap drawing, batching, and asset loading for 2D.
2. **A single blessed game loop** — fixed timestep, pause, scene transitions, and lifecycle hooks documented in one place.
3. **Entity or component model** — optional but common for scaling beyond a handful of `GameObject`s.
4. **Editor or tooling** — not required for a library, but absence limits adoption for designers.
5. **Cross-platform parity** — audio and input behave consistently; samples already branch (e.g. console audio on Windows).

The codebase partially addresses (2) via `Simulator` and samples, but **there is no first-class `GameLoop` API** that ties `Update`/`Draw`, delta time, and renderer frame time the same way across Raylib-windowed apps and `Simulator`-driven apps.

## Strategic recommendations

### Short term (clarity and consistency)

- **Document the “happy path”** for new games: Raylib window + `GameEngine` vs `GameEngine2D` vs `Simulator`, and when to use `NullInputSource`.
- **Align samples** on one loop style where possible, or add a tiny shared `SampleLoop` helper to reduce copy-paste and `Console.WriteLine` noise in hot paths.
- **Expand [critical-improvements.md](critical-improvements.md)** with a visible backlog table (this evaluation’s technical companion lists candidate items).

### Medium term (capability)

- **2D textures** on `Sprite2D` (or a parallel type) with Raylib and MonoGame implementations—this unlocks most casual 2D titles.
- **Optional fixed timestep** helper shared by Core or samples, with accumulation and max catch-up to avoid spiral of death.
- **Lightweight scene transition** story (load/unload, fade optional)—even as patterns, not full framework.

### Long term (optional product bets)

- **Incremental source generator** migration for assets (already noted in repo rules) to satisfy analyzer rules and reduce maintenance risk.
- **Stronger physics story** only if the project wants to compete on simulation; otherwise keep physics minimal and document “bring your own” (e.g. Bepu, Jolt bindings).

## Success metrics (suggested)

Use these to judge whether improvements are paying off:

- Time for a new contributor to run a sample and understand **where Update runs** (target: under 15 minutes with docs).
- Number of **backend-specific** code paths a typical 2D game must write (target: decrease over time).
- **Test coverage** for public APIs that ship on NuGet (rendering contracts, primitives invariants, physics edge cases).

## Conclusion

Frank.GameEngine is **well scoped as a modular library** with clean separation between model, rendering contracts, and backends. The main product risk is **expectation mismatch**: calling it a “game engine” invites comparison with integrated tools that include asset pipelines, editors, and full 2D/3D feature sets. Continuing to **narrow the pitch** (“simulation + pluggable rendering for .NET games”) or **invest deliberately in 2D presentation and loop ergonomics** will do the most to align perception with reality.

For a file-by-file style improvement list, see [evaluation-technical-improvements.md](evaluation-technical-improvements.md).
