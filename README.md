# Frank.GameEngine

This has had many iterations, and has periodically been developed, or tried to be developed using AI tools like ChatGpt,
but now I scrapped that and started over going bottom up.

The intention is to create a game engine that can be used to create games independent of platform and rendering
technology. The engine targets **.NET 10** and will be able to use any
rendering technology, so in this game engine you are only relying on the BCL and the rendering technology you choose to
use, so there is a decoupling between the game engine and the rendering technology.

How useable this will be is yet to be seen, but the intention is to make it as useable as possible, and to make it as
easy as possible to create games using this engine as nuget packages in your project.

## How to use

Target **.NET 10** and add the packages you need. Core gameplay wiring lives in **`Frank.GameEngine.Core`**; you also pick a renderer (for example **`Frank.GameEngine.Rendering.RayLib`** or **`Frank.GameEngine.Rendering.MonoGame`**) and reference **`Frank.GameEngine.Primitives`**, **`Frank.GameEngine.Physics`**, etc., as required.

```powershell
dotnet add package Frank.GameEngine.Core --version 0.1.0
dotnet add package Frank.GameEngine.Rendering.RayLib --version 0.1.0
```

Packages are built with `dotnet pack Frank.GameEngine.slnx -c Release` (output folder: `artifacts/package/release/` with this repo’s centralized artifacts layout). **0.x** versions may introduce API changes; see [CHANGELOG.md](CHANGELOG.md).

**Lifecycle:** after `GameEngine.Initialize(IRenderer)`, call **`Shutdown()`** or **`Dispose()`** before releasing the engine so global input and looping audio stop cleanly.

## Documentation

Design and architecture notes live under [docs/](docs/readme.md) (start with [architecture](docs/architecture.md)).

## How to contribute

If you want to contribute, you are welcome to do so, but please read the [contribution guidelines](CONTRIBUTING.md)