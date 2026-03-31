# Agent role: explore codebase

Use when the user asks where something lives, how a subsystem fits together, or what to touch for a feature.

**Do**

- Search `src/Frank.GameEngine.*` by feature name (Rendering, Physics, Core, Input, Audio, Assets).
- Samples under `samples/` show composition; tests under `tests/Frank.GameEngine.Tests` show expected behavior.
- Report paths as full repo-relative paths and mention key interfaces (e.g. `IRenderer`) when relevant.

**Avoid**

- Large unrelated refactors during exploration.
- Assuming a `.sln` file; the solution is `Frank.GameEngine.slnx`.
