# Critical improvements

This document tracks design fixes that were identified in review and how they were addressed.

## Completed

### 0. Unified `net10.0` and clean Release build

**Problem:** The source generator used `netstandard2.1` and a `VersionOverride` for Roslyn; multiple nullable and API warnings appeared in Release builds.

**Fix:** Generator targets **net10.0**, uses the central `Microsoft.CodeAnalysis.CSharp` package with `EnforceExtendedAnalyzerRules` disabled and `NoWarn` for RS1035/RS1041/RS1042 (legacy `ISourceGenerator`); nullable and API fixes across Audio, Primitives, Core, Rendering, samples, and tests; removed a machine-specific `None Remove` entry from `Frank.GameEngine.Audio.csproj`.

### 1. Force aggregation crash (`Frank.GameEngine.Physics.PhysicsEngine`)

**Problem:** When `Forces` was non-empty but every `Calculate` returned `null`, `Enumerable.Aggregate` ran on an empty sequence and threw.

**Fix:** Collect nullable results, then apply `Aggregate` only when `forceContributions.Count > 0`.

**File:** `src/libraries/Frank.GameEngine.Physics/PhysicsEngine.cs`

**Regression test:** `tests/Frank.GameEngine.Tests/Physics/PhysicsEngineTests.cs` — all forces return null must not throw.

### 2. Name clash: two “PhysicsEngine” types

**Problem:** `Frank.GameEngine.Rendering.RayLib` exposed a `BackgroundService` named `PhysicsEngine`, identical in name to `Frank.GameEngine.Physics.PhysicsEngine`, causing confusion and wrong mental model.

**Fix:**

- Renamed hosted service to `**RayLibHostedPhysicsService`** with XML docs stating it is not core physics.
- Renamed registration to `**AddRayLibHostedPhysics()**`; `AddGameEngine()` calls this internally.

**Files:**

- `src/libraries/Frank.GameEngine.Rendering.RayLib/Other/RayLibHostedPhysicsService.cs` (replaces `PhysicsEngine.cs`)
- `src/libraries/Frank.GameEngine.Rendering.RayLib/Other/ServiceCollectionExtensions.cs`

### 3. `GameEngine` initialization errors

**Problem:** Missing scene used `throw new Exception`; `Draw()` could run before `Initialize` and null-reference `_renderer`.

**Fix:**

- `Initialize`: `InvalidOperationException` with a clear message if no current scene.
- `Draw`: `InvalidOperationException` if `Initialize` has not run (`_renderer` is still null). `IRenderer?` removes the uninitialized-field warning on `GameEngine`.

**File:** `src/libraries/Frank.GameEngine.Core/GameEngine.cs`

### 4. Documentation

- [architecture.md](architecture.md) — layers and the two execution models.
- This file — audit trail for critical fixes.

### 5. Production packaging and lifecycle (0.1.0)

**Problem:** Libraries had no unified package version or descriptions; `AudioPlayer.PlayLooping`/`Stop` threw `NotImplementedException`; `GameEngine` used fire-and-forget `Thread` without shutdown; Wavefront `*.obj` assets were gitignored so CI could not generate `AdditionalResources2` (addressed in a prior CI fix).

**Fix:**

- Root `Directory.Build.props` (path condition for `src/`) + `build/PackageDescriptions.props`: **Version 0.1.0**, tags, and NuGet descriptions for shipping projects; **Rendering.Experimental** is not packed.
- `AudioPlayer`: thread-safe looping playback with NAudio; `Play` stops any active loop and no longer disposes the device before playback runs.
- `GameEngine`: `Shutdown()` / `IDisposable`, `TaskCreationOptions.LongRunning` for background work, guarded re-`Initialize`, `ObjectDisposedException` after `Dispose`.
- CI: `dotnet pack` on the solution after build.
- Root `PackageRequireLicenseAcceptance` set to **false**; `PackageProjectUrl` fixed.

### 6. Headless primitives, input seam, Raylib naming, CI audit

**Problem:** `System.Drawing` on `Shape`/`Scene` tied the model to Windows-era types; `GameEngine` exposed only `InputManager`; Raylib used `PhysicsEngineSignoff`; NuGet security advisories were suppressed even on CI.

**Fix:**

- **`Rgba32`**, **`IntPoint`**, **`IntRect`** in Primitives; **`IInputSource`** with **`InputManager`** as default; **`GameEngine.Input`** property.
- Raylib channel message renamed to **`RayLibPhysicsStepComplete`**.
- **`Directory.Build.props`**: **`NU1901`–`NU1904`** are warnings locally only; on **`CI` / `GITHUB_ACTIONS` / `TF_BUILD`** they fail the build with **`TreatWarningsAsErrors`**.
- XML docs on **`GameEngine`** describe host loop vs background input/audio.

## Recommended next (not done here)

| Item                             | Rationale                                                                                                    |
| -------------------------------- | ------------------------------------------------------------------------------------------------------------ |
| **Single game loop / threading** | Optionally replace background `Task` input/audio with a fully host-owned loop and injectable schedulers.     |

## References

- Raylib hosted pipeline diagram: `src/libraries/Frank.GameEngine.Rendering.RayLib/README.md` (update class names to `RayLibHostedPhysicsService` / `AddRayLibHostedPhysics` when editing).

