# Implementation Plan: CQRS Library (Adom.CQRS)

**Branch**: `001-cqrs-library` | **Date**: 2026-01-17 | **Spec**: [spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-cqrs-library/spec.md`

## Summary

High-performance CQRS library for .NET 10 providing request/handler dispatching with optional caching via `IDistributedCache`. The library features pipeline behaviors for cross-cutting concerns, dual handler discovery modes (assembly scanning + source generators), and zero-allocation patterns on hot paths. Published as `Adom.CQRS` NuGet package with optional `Adom.CQRS.SourceGenerators` companion package.

## Technical Context

**Language/Version**: C# 13+ / .NET 10 (net10.0)
**Primary Dependencies**:
- `Microsoft.Extensions.DependencyInjection.Abstractions` (DI integration)
- `Microsoft.Extensions.Caching.Abstractions` (IDistributedCache)
- `System.Text.Json` (cache key serialization)

**Storage**: N/A (library delegates to host-provided IDistributedCache)
**Testing**: xUnit, BenchmarkDotNet
**Target Platform**: .NET 10+ (cross-platform: Windows, Linux, macOS)
**Project Type**: NuGet library package (multi-project solution)
**Performance Goals**:
- <1ms dispatch overhead
- Zero allocations on hot paths
- <1ms cache retrieval
**Constraints**:
- Native AOT compatible
- No reflection in source generator mode
- Minimal public API surface
**Scale/Scope**: Single library, ~15-20 public types

## Constitution Check

*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

| Principle | Status | Evidence |
|-----------|--------|----------|
| I. Performance Above All | ✅ PASS | Zero-allocation patterns, Span<T>, aggressive inlining planned |
| II. Modern .NET Only | ✅ PASS | .NET 10 only, latest C# features |
| III. Algorithmic Excellence | ✅ PASS | Efficient hash-based cache keys, O(1) handler lookup |
| IV. Test Coverage Required | ✅ PASS | xUnit + BenchmarkDotNet specified |
| V. Type Safety & Null Safety | ✅ PASS | Nullable reference types, ThrowHelper pattern |
| VI. Clean API Surface | ✅ PASS | Minimal public types, fluent DI registration |

**Gate Result**: PASS - No violations. Proceed to Phase 0.

## Project Structure

### Documentation (this feature)

```text
specs/001-cqrs-library/
├── plan.md              # This file
├── spec.md              # Feature specification
├── research.md          # Phase 0 output
├── data-model.md        # Phase 1 output
├── quickstart.md        # Phase 1 output
├── contracts/           # Phase 1 output (API contracts)
├── checklists/          # Quality checklists
│   └── requirements.md
└── tasks.md             # Phase 2 output (/speckit.tasks command)
```

### Source Code (repository root)

```text
Adom.Framework/
├── src/
│   ├── Adom.CQRS/                              # Core library
│   │   ├── Adom.CQRS.csproj
│   │   ├── Abstractions/
│   │   │   ├── IRequest.cs                     # Request interfaces
│   │   │   ├── IHandler.cs                     # Handler interfaces
│   │   │   ├── IDispatcher.cs                  # Dispatcher interface
│   │   │   ├── IPipelineBehavior.cs            # Pipeline behavior interface
│   │   │   └── ICacheableRequest.cs            # Caching marker interface
│   │   ├── Dispatching/
│   │   │   ├── Dispatcher.cs                   # Core dispatcher implementation
│   │   │   ├── HandlerRegistry.cs              # Handler registration/lookup
│   │   │   └── PipelineBuilder.cs              # Pipeline chain builder
│   │   ├── Caching/
│   │   │   ├── CachingBehavior.cs              # Built-in caching pipeline behavior
│   │   │   ├── CacheKeyGenerator.cs            # Default cache key generation
│   │   │   └── CacheOptions.cs                 # Cache configuration
│   │   ├── Discovery/
│   │   │   └── AssemblyScanner.cs              # Runtime handler discovery
│   │   ├── Exceptions/
│   │   │   ├── HandlerNotFoundException.cs
│   │   │   └── DispatcherException.cs
│   │   ├── Extensions/
│   │   │   └── ServiceCollectionExtensions.cs  # DI registration
│   │   └── Internal/
│   │       └── ThrowHelper.cs                  # Centralized exception throwing
│   │
│   └── Adom.CQRS.SourceGenerators/             # Optional source generator package
│       ├── Adom.CQRS.SourceGenerators.csproj
│       └── HandlerRegistrationGenerator.cs     # Compile-time handler discovery
│
└── tests/
    ├── Adom.CQRS.Tests/                        # Unit tests
    │   ├── Adom.CQRS.Tests.csproj
    │   ├── DispatcherTests.cs
    │   ├── PipelineBehaviorTests.cs
    │   ├── CachingBehaviorTests.cs
    │   └── HandlerDiscoveryTests.cs
    │
    └── Adom.CQRS.Benchmarks/                   # Performance benchmarks
        ├── Adom.CQRS.Benchmarks.csproj
        ├── DispatchBenchmarks.cs
        └── CachingBenchmarks.cs
```

**Structure Decision**: Multi-project NuGet library structure with separate core library and optional source generators package. Tests and benchmarks in dedicated projects under `/tests/`.

## Complexity Tracking

> No violations detected. No justifications needed.

| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|-------------------------------------|
| N/A | N/A | N/A |
