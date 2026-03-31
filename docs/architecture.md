# Architecture

## Layers

| Layer | Projects | Depends on |
|--------|-----------|------------|
| Model | `Frank.GameEngine.Primitives` | BCL, `System.Drawing` for colors |
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

Input and audio are started from `GameEngine.Initialize(IRenderer)` on **background threads** (global hook + audio loop). This model is simple but does not use the Generic Host pipeline.

### 2. Raylib + Generic Host experiment

`Frank.GameEngine.Rendering.RayLib` can register **hosted services** (`AddGameEngine` in `ServiceCollectionExtensions`) that use **channels**: `TickProducer` → **`RayLibHostedPhysicsService`** → `RenderQueue` → `RenderLoop`.

That hosted physics step is **not** the core `PhysicsEngine`; it is a **demo pipeline** for ticks and signoff messages. Renamed from `PhysicsEngine` to avoid confusion with simulation in `Frank.GameEngine.Physics`.

## Extension points

- **Collisions:** `ICollisionHandler`
- **Forces:** `IForce` + `PhysicsEngine.Forces`
- **Drawing:** implement `IRenderer` in a new project referencing `Frank.GameEngine.Rendering`

See [critical-improvements.md](critical-improvements.md) for completed fixes and follow-up work.
