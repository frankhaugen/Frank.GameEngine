# Changelog

All notable changes to this project are documented here. The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added

- **2D engine path:** **`Transform2D`**, **`Sprite2D`**, **`Camera2D`**, **`GameObject2D`**, **`Scene2D`**, **`Scene2DManager`**, **`GameEngine2D`**, **`IRenderer2D`**, **`RayLibRenderer2D`**, **`MonoGameRenderer2D`**, **`RayLibRenderer.Underlay2D` / `Overlay2D`** for 3D+HUD. Sample **`Frank.GameEngine.Samples.Hello2D`**.
- **`TriangleMesh`** + **`Shape.TriangleMesh`**; **OBJ** face parsing; **`SceneMeshImporter`** (Assimp: FBX, glTF, OBJ, …); **`FpsCameraState`**; **`NullInputSource`** for Raylib-poll loops.
- Sample **`Frank.GameEngine.Samples.Fps`**: Assimp-loaded level/crate, first-person movement, **`RayLibRenderer.ShouldClose`** / **`FrameDeltaSeconds`**.
- Tests for mesh/OBJ/Assimp/teapot; **`TriangleMeshBenchmark`** (BenchmarkDotNet).

### Changed

- **Teapot** and embedded models load via **`AdditionalResources2`** into **`TriangleMesh`** (correct topology vs. old vertex-only fan).
- **Raylib** camera mode **Custom** (scene-owned **`Camera`**); **`Camera.FieldOfView`** documented and defaulted in **degrees** (fixes MonoGame perspective and Raylib FOV consistency).
- Central package refresh: **TUnit** 1.24.13; pinned transitive **NVorbis** 0.10.5 and **MonoGame.Library.*** SDL/OpenAL stacks.
- GitHub Actions: **checkout** v5, **setup-dotnet** v5, **upload-artifact** v5 on `verify-dotnet`.
- Root **.editorconfig** for current C# style (file-scoped namespaces, collection expressions, etc.).
- NuGet package copyright year range in `Directory.Build.props`.

## [0.1.0] - 2026-03-31

### Added

- NuGet-ready **0.1.0** versioning and **per-package descriptions** for shipping libraries under `src/`.
- `GameEngine.Shutdown()` and `IDisposable` for coordinated teardown of background input/audio work.
- `dotnet pack` verification in CI.

### Fixed

- `Frank.GameEngine.Audio.Ogg.AudioPlayer`: implemented `PlayLooping` and `Stop` (previously `NotImplementedException`); `Play` no longer disposes the output device before playback can start.
- `PackageProjectUrl` corrected to `https://github.com/frankhaugen/Frank.GameEngine`.
- `PackageRequireLicenseAcceptance` set to `false` for typical OSS consumption.

### Changed

- `Frank.GameEngine.Rendering.Experimental` is **not** packed (`IsPackable=false`).

