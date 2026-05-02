---
name: frank-gameengine-verify
description: Restore, build, and test Frank.GameEngine.slnx in Release using TUnit and Microsoft.Testing.Platform.
---

# Frank.GameEngine — verify

Use from the repository root (`Frank.GameEngine.slnx`).

## Commands

```powershell
dotnet restore Frank.GameEngine.slnx
dotnet build Frank.GameEngine.slnx -c Release
dotnet test --solution Frank.GameEngine.slnx -c Release
```

## Requirements

- SDK per [global.json](../../../global.json); `global.json` must keep `"test": { "runner": "Microsoft.Testing.Platform" }` for `dotnet test` with TUnit.

## After success

If the user asked to ship: commit and push per `.cursor/rules/git-ship-after-verify.mdc`.
