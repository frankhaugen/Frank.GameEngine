# Shipping libraries (NuGet)

Packable **Frank.GameEngine.\*** assemblies live here. They share NuGet metadata from the repo root [`Directory.Build.props`](../../Directory.Build.props) (version, icon, license) and per-package descriptions in [`build/PackageDescriptions.props`](../../build/PackageDescriptions.props).

| Project / package | Role |
|-------------------|------|
| `Frank.GameEngine.Primitives` | Scenes, transforms, meshes, boards, color types |
| `Frank.GameEngine.Rendering` | `IRenderer` / `IRenderer2D` contracts |
| `Frank.GameEngine.Physics` | Forces, collision hooks |
| `Frank.GameEngine.Audio` | Audio abstractions and playback |
| `Frank.GameEngine.Input` | Input sources (e.g. SharpHook, null source) |
| `Frank.GameEngine.Core` | `GameEngine`, `GameEngine2D`, `Simulator`, scene managers |
| `Frank.GameEngine.Assets` | OBJ/Assimp import, generated resource helpers |
| `Frank.GameEngine.Rendering.RayLib` | Raylib renderer + optional host integration |
| `Frank.GameEngine.Rendering.MonoGame` | MonoGame renderer |

**Pack** from repo root:

```powershell
dotnet pack Frank.GameEngine.slnx -c Release
```

Output: `artifacts/package/release/*.nupkg`.

Non-shipping experiments live under [`src/experimental/`](../experimental/) (not packed).
