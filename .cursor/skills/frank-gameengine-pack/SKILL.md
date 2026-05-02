---
name: frank-gameengine-pack
description: Pack shipping NuGet libraries from Frank.GameEngine.slnx and locate output artifacts.
---

# Frank.GameEngine — pack

## Command

From repo root:

```powershell
dotnet pack Frank.GameEngine.slnx -c Release
```

For CI-like metadata:

```powershell
dotnet pack Frank.GameEngine.slnx -c Release -p:ContinuousIntegrationBuild=true
```

## Output

Packages land under **`artifacts/package/release/`** (see `Directory.Build.props` `UseArtifactsOutput` / `ArtifactsPath`).

## Scope

Shipping libraries live under `src/Frank.GameEngine.*` with packaging metadata from `build/PackageDescriptions.props`. **Rendering.Experimental** is not packed.
