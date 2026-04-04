# Architecture

All shipping projects, tests, samples, and the **Roslyn assets generator** target **net10.0** (see repository `Directory.Build.props` and `tools/Frank.GameEngine.Generators.AssetsGenerator`).

## Layers

| Layer | Projects | Depends on |
|--------|-----------|------------|
| Model | `Frank.GameEngine.Primitives` | BCL; `Rgba32`, `IntPoint`, `IntRect`; **3D:** `Polygon`, `TriangleMesh`, `Shape`, `FpsCameraState`; **2D:** `Transform2D`, `Sprite2D`, `Camera2D`, `GameObject2D`, `Scene2D` |
| Rendering contract | `Frank.GameEngine.Rendering` | Primitives only; **`IRenderer`** (3D), **`IRenderer2D`** (2D) |
| Backends | `Frank.GameEngine.Rendering.*` | Rendering + backend SDKs (Raylib 3D+2D, MonoGame 3D+2D, SVG, console, …) |
| Simulation | `Frank.GameEngine.Physics` | Primitives |
| Input / Audio | `Frank.GameEngine.Input`, `Frank.GameEngine.Audio` | (Input: SharpHook) |
| Composition | `Frank.GameEngine.Core` | Physics, Rendering, Input, Audio, Primitives; **`GameEngine`** / **`GameEngine2D`** |
| Assets | `Frank.GameEngine.Assets` | Primitives, Roslyn generator, **AssimpNet** (FBX/OBJ/glTF/… → `TriangleMesh`) |

**`IRenderer`** is the seam for **3D** scenes (`Scene`, `GameObject`, meshes). **`IRenderer2D`** is the seam for **orthographic 2D** (`Scene2D`, `GameObject2D`, solid sprites today; textures can plug in later).

Core’s **`GameEngine`** wires physics, input, audio, **3D** scenes, and an `IRenderer`. **`GameEngine2D`** mirrors the same input/audio bootstrap for **`Scene2D`** + **`IRenderer2D`**.

**Raylib:** **`RayLibRenderer`** can draw optional **`Underlay2D`** / **`Overlay2D`** (`Scene2D`) around the 3D pass for parallax or HUD. **`RayLibRenderer2D`** is a standalone 2D window. **MonoGame:** **`MonoGameRenderer`** / **`MonoGameRenderer2D`** follow the same split.

## Two execution models (important)

### 1. Application loop (samples and typical games)

`Simulator` (or any host loop) calls:

- `GameEngine.Update(UpdateArgs)` → **`Frank.GameEngine.Physics.PhysicsEngine`**
- `GameEngine.Draw()` → `IRenderer.Render(Scene)`

Input and audio are started from `GameEngine.Initialize(IRenderer)` on **long-running background tasks** (global hook + `PlayLooping`). Use **`NullInputSource`** when the windowing API polls keys/mouse (e.g. Raylib in **`Frank.GameEngine.Samples.Fps`**) so a global hook is not started. Call **`GameEngine.Shutdown()`** or **`Dispose()`** before releasing the engine so hooks and looping audio stop. This model is simple but does not use the Generic Host pipeline.

### 2. Raylib + Generic Host experiment

`Frank.GameEngine.Rendering.RayLib` can register **hosted services** (`AddGameEngine` in `ServiceCollectionExtensions`) that use **channels**: `TickProducer` → **`RayLibHostedPhysicsService`** → `RenderQueue` → `RenderLoop`.

That hosted physics step is **not** the core `PhysicsEngine`; it is a **demo pipeline** for ticks and `RayLibPhysicsStepComplete` messages. Renamed from `PhysicsEngine` to avoid confusion with simulation in `Frank.GameEngine.Physics`.

## 3D mesh and transforms

- **`Polygon`:** Closed vertex loop; fan-triangulation from vertex 0 is only correct for simple discs/pyramids—not for arbitrary OBJ/FBX topology.
- **`TriangleMesh`:** Vertex buffer + triangle index list; used for correct **OBJ `f`** parsing and **Assimp** (`SceneMeshImporter`) output. When **`Shape.TriangleMesh`** is set, **Raylib** and **MonoGame** renderers draw it instead of **`Polygon.Faces`**.
- **`Shape.Transform` / `GetTransformedShape`:** Applies **scale → rotation → translation** to both mesh and polygon data. **`Shape.Intersect`** uses **AABB overlap** when either side has a **`TriangleMesh`** (mesh–mesh is not full triangle collision).
- **`Camera.FieldOfView`:** Vertical FOV in **degrees** (MonoGame `CreatePerspectiveFieldOfView` and Raylib agree).
- **`Camera.Up`:** Mutable so you can set a custom up vector for non-default orientations.
- **`Polygon.GetAxisAlignedBoundingBox` / `TriangleMesh`:** Broad-phase and culling helpers.

## Extension points

- **Collisions:** `ICollisionHandler`
- **Forces:** `IForce` + `PhysicsEngine.Forces`
- **Drawing:** implement `IRenderer` in a new project referencing `Frank.GameEngine.Rendering`

See [critical-improvements.md](critical-improvements.md) for completed fixes and follow-up work.
