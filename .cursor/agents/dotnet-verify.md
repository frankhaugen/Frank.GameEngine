# Agent role: verify .NET change

Use after substantive C# or project file edits.

**Steps**

1. `dotnet restore Frank.GameEngine.slnx`
2. `dotnet build Frank.GameEngine.slnx -c Release`
3. `dotnet test Frank.GameEngine.slnx -c Release` (when tests cover the change)

**If restore fails with NU1507**

- Central package management requires explicit package source mapping in `nuget.config` when multiple feeds exist.

**If the generator project fails**

- It targets `net10.0` with central `Microsoft.CodeAnalysis.CSharp`. Upgrading that package may require addressing RS analyzer rules or migrating to `IIncrementalGenerator`.
