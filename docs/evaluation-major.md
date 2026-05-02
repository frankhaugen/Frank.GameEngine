# Frank.GameEngine — major evaluation (executive view)

This page synthesizes [architecture.md](architecture.md), [critical-improvements.md](critical-improvements.md), [evaluation-product-and-roadmap.md](evaluation-product-and-roadmap.md), and [evaluation-technical-improvements.md](evaluation-technical-improvements.md) into **prioritized bets**. Use it when deciding what to build next; deep dives stay in the linked documents.

## Positioning (one sentence)

Frank.GameEngine is a **small, testable simulation and rendering toolkit** for .NET with pluggable backends—not a turnkey editor stack; scope and limitations are documented on purpose.

## Theme matrix

| Theme | Reality today | Highest-leverage follow-ups |
|--------|----------------|------------------------------|
| **Product fit** | Library-first simulation + pluggable renderers; honest limitations | Narrow the pitch **or** invest deliberately in **2D presentation** (textures, batching) and a **single documented game-loop story** |
| **Engineering risk** | Two execution models (Raylib hosted/DI pipeline vs application-owned loop) confuse newcomers | Decision-tree docs; optional **host-owned loop / scheduler** (see “Recommended next” in [critical-improvements.md](critical-improvements.md)) |
| **Maintainability** | Assets Roslyn generator still uses `ISourceGenerator` with targeted NoWarn | Migrate to **`IIncrementalGenerator`** when that code path is touched |
| **Samples as curriculum** | Mixed patterns, noisy logging, large single-file samples | Shared minimal loop helper, quiet-by-default logging, split large samples over time |
| **CI and supply chain** | Strong build / pack / test matrix; NU190x strict on CI | Keep CI strict; any new NuGet feeds require [nuget.config](../nuget.config) source mapping |

## Dev orchestration (.NET Aspire)

An **Aspire AppHost** under `dev/` is a **developer convenience**: one entry point to build samples and start **one** windowed executable from the dashboard with structured logs. It does **not** change engine architecture, replace a documented game loop, or fix rendering gaps—see the table above for product/engineering priorities.

## Where to read next

| Audience | Document |
|----------|-----------|
| Architecture and runtime models | [architecture.md](architecture.md) |
| Shipped fixes and small backlog | [critical-improvements.md](critical-improvements.md) |
| Maturity and strategy | [evaluation-product-and-roadmap.md](evaluation-product-and-roadmap.md) |
| File-level improvement ideas | [evaluation-technical-improvements.md](evaluation-technical-improvements.md) |

## Suggested success metrics

From the product evaluation: time for a new contributor to run a sample and know **where `Update` runs**; reduction of backend-specific glue in typical 2D games; growing tests around **public contracts** (primitives, renderer seams, physics edge cases).
