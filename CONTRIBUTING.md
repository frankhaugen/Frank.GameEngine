# Contributing

Development workflow and conventions for AI and human contributors are documented in [AGENTS.md](AGENTS.md).

**Quick checks**

```powershell
dotnet build Frank.GameEngine.slnx -c Release
dotnet test Frank.GameEngine.slnx -c Release
```

Open pull requests against `main`. CI uses reusable workflows from `frankhaugen/Workflows`; ensure the shared workflow targets `Frank.GameEngine.slnx` if it accepts a solution path.
