# Frank.GameEngine — technical evaluation and improvement backlog

This document lists **concrete** improvement areas grounded in the current codebase and [architecture.md](architecture.md). It is not a commitment order; prioritize based on your product goals.

## Rendering

### 2D: textures and batching

- **Today:** `Sprite2D` is documented as solid-color quads; Raylib draws via `DrawRectanglePro`-style paths (`Raylib2DDrawing`).
- **Improve:** Add optional texture reference + UVs (or a `TexturedSprite2D`) and implement in **Raylib** and **MonoGame** 2D renderers. Consider **sprite batching** if many quads are used (tile maps, particles).
- **Why:** Most shipped 2D games need bitmaps; without them, samples stay abstract “colored rectangles.”

### 3D: mesh and materials

- **Today:** `TriangleMesh` vs `Polygon` split is documented; materials are effectively flat color per shape.
- **Improve:** Define a minimal **material / shader hook** or PBR-lite parameters if you want reusable 3D assets beyond flat shading—keep it optional to avoid over-engineering.

### Renderer contract

- **Today:** `IRenderer` / `IRenderer2D` are small and good boundaries.
- **Improve:** Consider **frame stats** (optional callback or struct) for debugging: draw calls, triangle count—useful for samples and perf regressions.

## Simulation and physics

- **Forces:** `PhysicsEngine` force aggregation was hardened (see [critical-improvements.md](critical-improvements.md)); similar **defensive patterns** help when extending with more force types.
- **Collision:** `ICollisionHandler` exists; **mesh–mesh** is AABB-only when `TriangleMesh` is involved—document clearly in API docs for `Shape.Intersect` callers.
- **Polygon triangulation:** Fan from vertex 0 is **not** valid for arbitrary meshes—architecture already warns; **guard or validate** in debug builds for imported topology if users hit silent garbage triangles.

## Core: lifecycle, threading, and loops

- **Background tasks:** Input and audio start on **long-running tasks** from `Initialize`. This is simple but surprises developers who expect a single-threaded game thread.
- **Improve:** As noted in critical improvements, a **host-owned loop** option (or injectable scheduler) would reduce surprise and ease embedding in UI frameworks or custom hosts.
- **Raylib windowed samples:** Some use **`Simulator`** with a fixed `TimeIncrement` while the window also has real frame time—**document** which clock drives physics vs rendering to avoid subtle desync when copying sample code.
- **`GameEngine2D`:** No `Update` equivalent wired like 3D `GameEngine.Update`; game logic often lives in the host loop (e.g. Battleship). Either **add a documented pattern** or a thin **`Update2D`** hook if 2D games should follow the same lifecycle as 3D.

## Two execution models (Raylib host vs application loop)

- **Risk:** Developers can confuse **`RayLibHostedPhysicsService`** / channels with **`Frank.GameEngine.Physics.PhysicsEngine`** (renaming helped; README cross-links should stay accurate).
- **Improve:** A **decision tree** in docs (“I want X → use Y”) reduces misuse. Optionally **deprecate or isolate** the experimental host pipeline behind a separate package or clearer namespace if it is not first-class.

## Assets and code generation

- **Generator:** Still on `ISourceGenerator` with targeted `NoWarn`; migration to **`IIncrementalGenerator`** is the right long-term fix for analyzer compliance and IDE performance.
- **Assimp / mesh import:** Good for prototyping; **document format support limits** and failure modes (large assets, licensing of shipped content).

## Input

- **`IInputSource` / `NullInputSource`:** Good for Raylib-polling hosts; ensure every **Raylib sample** that polls keys uses `NullInputSource` to avoid duplicate global hooks (Pong/BouncingBall patterns should stay the reference).

## Audio

- **Platform split** (e.g. `ConsoleAudioPlayer` on Windows, `SilentAudioPlayer` elsewhere) is documented; **samples** should keep this pattern consistent and avoid implying sound on all OSes.

## Testing

- **Strengths:** TUnit + FluentAssertions 7.x policy is documented; physics regression tests exist.
- **Improve:** Add **contract tests** for renderers where feasible (e.g. golden images are heavy; instead test **transform math**, `GetTransformedShape`, camera matrices). **Integration tests** for “initialize renderer headless” may be limited by native backends—document what CI can and cannot cover.

## Samples and developer experience

- **Inconsistency:** Some samples spam `Console.WriteLine` every frame; others are quiet. **Tone down** or gate behind a debug flag for teaching clarity.
- **Battleship:** Large `Program.cs` with game rules and UI; **splitting** into files would model how real games organize code.
- **New sample suggestion:** A minimal **“one file + one loop”** template that uses **renderer `FrameDeltaSeconds`** for movement and documents **fixed vs variable** timestep.

## Security and supply chain

- **NU190x** handling on CI vs local is documented in critical improvements—**keep CI strict**; review any new package additions under CPM.

## Documentation debt

- **Link** evaluation docs from the main docs index (see [index.md](index.md)).
- **API docs:** Public surface is large; focus XML docs on **seams** (`IRenderer*`, `GameEngine` lifecycle, physics limitations) first.

## Summary table

| Priority | Item | Effort (rough) |
|----------|------|----------------|
| High | 2D textures + loading path | Medium–large |
| High | Single documented game-loop story (3D + 2D) | Small–medium |
| Medium | `GameEngine2D` lifecycle parity with 3D | Small–medium |
| Medium | Sample cleanup (logging, structure) | Small |
| Medium | Incremental generator migration | Medium |
| Low | Optional material/shader hook for 3D | Large |
| Low | Host-owned scheduler / loop option | Medium |

For strategic framing and positioning, see [evaluation-product-and-roadmap.md](evaluation-product-and-roadmap.md).
