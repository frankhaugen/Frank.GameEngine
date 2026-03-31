# Agent role: verify .NET change

Use after substantive C# or project file edits.

**Steps**

1. `dotnet restore Frank.GameEngine.slnx`
2. `dotnet build Frank.GameEngine.slnx -c Release`
3. `dotnet test Frank.GameEngine.slnx -c Release` (when tests cover the change)

**If restore fails with NU1507**

- Central package management requires explicit package source mapping in `nuget.config` when multiple feeds exist.

**If the generator project fails**

- It targets `netstandard2.1` with Roslyn `4.8.0` via `VersionOverride`; do not upgrade the analyzer package without checking RS rules and API surface.
