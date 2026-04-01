# Architecture

All shipping projects, tests, samples, and the **Roslyn assets generator** target **net10.0** (see repository `Directory.Build.props` and `tools/Frank.GameEngine.Generators.AssetsGenerator`).

## Layers

| Layer | Projects | Depends on |
|--------|-----------|------------|
| Model | `Frank.GameEngine.Primitives` | BCL; `Rgba32`, `IntPoint`, `IntRect` (no `System.Drawing` on hot paths) |
| Rendering contract | `Frank.GameEngine.Rendering` | Primitives only |
| Backends | `Frank.GameEngine.Rendering.*` | Rendering + backend SDKs |
| Simulation | `Frank.GameEngine.Physics` | Primitives |
| Input / Audio | `Frank.GameEngine.Input`, `Frank.GameEngine.Audio` | (Input: SharpHook) |
| Composition | `Frank.GameEngine.Core` | Physics, Rendering, Input, Audio, Primitives |
| Assets | `Frank.GameEngine.Assets` | Primitives, Roslyn generator |

`IRenderer` is the main seam for “how we draw.” Core’s `GameEngine` is a **facade** that wires physics, input, audio, scenes, and a concrete renderer supplied at runtime.

## Two execution models (important)

### 1. Application loop (samples and typical games)

`Simulator` (or any host loop) calls:

- `GameEngine.Update(UpdateArgs)` → **`Frank.GameEngine.Physics.PhysicsEngine`**
- `GameEngine.Draw()` → `IRenderer.Render(Scene)`

Input and audio are started from `GameEngine.Initialize(IRenderer)` on **long-running background tasks** (global hook + `PlayLooping`). Call **`GameEngine.Shutdown()`** or **`Dispose()`** before releasing the engine so hooks and looping audio stop. This model is simple but does not use the Generic Host pipeline.

### 2. Raylib + Generic Host experiment

`Frank.GameEngine.Rendering.RayLib` can register **hosted services** (`AddGameEngine` in `ServiceCollectionExtensions`) that use **channels**: `TickProducer` → **`RayLibHostedPhysicsService`** → `RenderQueue` → `RenderLoop`.

That hosted physics step is **not** the core `PhysicsEngine`; it is a **demo pipeline** for ticks and `RayLibPhysicsStepComplete` messages. Renamed from `PhysicsEngine` to avoid confusion with simulation in `Frank.GameEngine.Physics`.

## 3D mesh and transforms

- **`Polygon`:** One edge per vertex (closed loop); `Translate` copies vertices and does not mutate the source mesh.
- **`Shape.Transform` / `GetTransformedShape`:** Applies **scale → rotation → translation** in that order (local vertices, then placed in world space). Raylib rendering uses **`GameObject.GetTransformedShape()`** so draws match physics/collision transforms.
- **`Camera.Up`:** Mutable so you can set a custom up vector for non-default orientations.
- **`Polygon.GetAxisAlignedBoundingBox`:** Shared helper for broad-phase collision (`CollisionDetector`) and other culling.

## Extension points

- **Collisions:** `ICollisionHandler`
- **Forces:** `IForce` + `PhysicsEngine.Forces`
- **Drawing:** implement `IRenderer` in a new project referencing `Frank.GameEngine.Rendering`

See [critical-improvements.md](critical-improvements.md) for completed fixes and follow-up work.
