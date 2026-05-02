---
name: frank-gameengine-sample
description: Run or explain Frank.GameEngine samples—loops, NullInputSource, audio, and optional Aspire AppHost.
---

# Frank.GameEngine — samples

## Direct run (single sample)

From repo root, run one executable project, for example:

```powershell
dotnet run --project samples/Frank.GameEngine.Samples.Pong/Frank.GameEngine.Samples.Pong.csproj -c Release
```

Replace the path with the sample under `samples/` (Pong, BouncingBall, Battleship, Fps, Hello2D).

## Concepts (teach correctly)

- **Two execution models**: Raylib hosted/DI pipeline vs application-owned loop with `Simulator` — see [docs/architecture.md](../../../docs/architecture.md) and [docs/evaluation-technical-improvements.md](../../../docs/evaluation-technical-improvements.md).
- **Input**: Raylib samples that poll keys should use **`NullInputSource`** where appropriate so global hooks are not duplicated.
- **Audio**: `ConsoleAudioPlayer` is Windows-only; elsewhere use **`SilentAudioPlayer`** (see `.cursor/rules/platform-audio.mdc` and AGENTS.md).

## Aspire AppHost (optional)

To build all samples and start **one** from the dashboard:

```powershell
dotnet run --project dev/Frank.GameEngine.Samples.AppHost/Frank.GameEngine.Samples.AppHost.csproj
```

Do not start multiple windowed games at once unless you know they cooperate.
