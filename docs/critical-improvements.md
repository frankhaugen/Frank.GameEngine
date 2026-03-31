# Critical improvements

This document tracks design fixes that were identified in review and how they were addressed.

## Completed

### 1. Force aggregation crash (`Frank.GameEngine.Physics.PhysicsEngine`)

**Problem:** When `Forces` was non-empty but every `Calculate` returned `null`, `Enumerable.Aggregate` ran on an empty sequence and threw.

**Fix:** Collect nullable results, then apply `Aggregate` only when `forceContributions.Count > 0`.

**File:** `src/Frank.GameEngine.Physics/PhysicsEngine.cs`

**Regression test:** `tests/Frank.GameEngine.Tests/Physics/PhysicsEngineTests.cs` — all forces return null must not throw.

### 2. Name clash: two “PhysicsEngine” types

**Problem:** `Frank.GameEngine.Rendering.RayLib` exposed a `BackgroundService` named `PhysicsEngine`, identical in name to `Frank.GameEngine.Physics.PhysicsEngine`, causing confusion and wrong mental model.

**Fix:**

- Renamed hosted service to `**RayLibHostedPhysicsService`** with XML docs stating it is not core physics.
- Renamed registration to `**AddRayLibHostedPhysics()**`; `AddGameEngine()` calls this internally.

**Files:**

- `src/Frank.GameEngine.Rendering.RayLib/Other/RayLibHostedPhysicsService.cs` (replaces `PhysicsEngine.cs`)
- `src/Frank.GameEngine.Rendering.RayLib/Other/ServiceCollectionExtensions.cs`

### 3. `GameEngine` initialization errors

**Problem:** Missing scene used `throw new Exception`; `Draw()` could run before `Initialize` and null-reference `_renderer`.

**Fix:**

- `Initialize`: `InvalidOperationException` with a clear message if no current scene.
- `Draw`: `InvalidOperationException` if `Initialize` has not run (`_renderer` is still null). `IRenderer?` removes the uninitialized-field warning on `GameEngine`.

**File:** `src/Frank.GameEngine.Core/GameEngine.cs`

### 4. Documentation

- [architecture.md](architecture.md) — layers and the two execution models.
- This file — audit trail for critical fixes.

## Recommended next (not done here)


| Item                              | Rationale                                                                                                                 |
| --------------------------------- | ------------------------------------------------------------------------------------------------------------------------- |
| **Single game loop / threading**  | Replace ad-hoc `Thread` usage in `Initialize` with a documented lifecycle (or host-driven loop) and coordinated shutdown. |
| **Input abstraction**             | Introduce something like `IInputSource` with a SharpHook implementation so Core does not assume global hooks everywhere.  |
| **Colors in Primitives**          | Replace or wrap `System.Drawing.Color` with an engine-owned type if you want a stricter “headless core” boundary.         |
| **Rename `PhysicsEngineSignoff`** | Optional: e.g. `RayLibPhysicsStepComplete` for parity with `RayLibHostedPhysicsService`.                                  |
| **CI / vulnerable packages**      | Treat high-severity NU advisories as errors in CI while keeping local overrides documented.                               |


## References

- Raylib hosted pipeline diagram: `src/Frank.GameEngine.Rendering.RayLib/README.md` (update class names to `RayLibHostedPhysicsService` / `AddRayLibHostedPhysics` when editing).

