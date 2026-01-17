# Tasks: CQRS Library (Adom.CQRS)

**Input**: Design documents from `/specs/001-cqrs-library/`
**Prerequisites**: plan.md, spec.md, research.md, data-model.md, contracts/

**Tests**: Tests are included as the project requires xUnit + BenchmarkDotNet per constitution.

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2, US3, US4)
- Include exact file paths in descriptions

## Path Conventions

Based on plan.md structure:
- **Core library**: `Adom.Framework/src/Adom.CQRS/`
- **Source generators**: `Adom.Framework/src/Adom.CQRS.SourceGenerators/`
- **Unit tests**: `Adom.Framework/tests/Adom.CQRS.Tests/`
- **Benchmarks**: `Adom.Framework/tests/Adom.CQRS.Benchmarks/`

---

## Phase 1: Setup (Project Structure)

**Purpose**: Create project structure and configure build

- [X] T001 Create Adom.CQRS.csproj with .NET 10 target in `Adom.Framework/src/Adom.CQRS/Adom.CQRS.csproj`
- [X] T002 [P] Create Adom.CQRS.Tests.csproj with xUnit in `Adom.Framework/tests/Adom.CQRS.Tests/Adom.CQRS.Tests.csproj`
- [X] T003 [P] Create Adom.CQRS.Benchmarks.csproj with BenchmarkDotNet in `Adom.Framework/tests/Adom.CQRS.Benchmarks/Adom.CQRS.Benchmarks.csproj`
- [X] T004 Add project references to Adom.Framework.sln
- [X] T005 [P] Configure NuGet package metadata in Adom.CQRS.csproj (PackageId, Authors, Description)
- [X] T006 [P] Create Directory.Build.props with shared settings (nullable, latest C#, implicit usings)

---

## Phase 2: Foundational (Core Abstractions)

**Purpose**: Core types that ALL user stories depend on - MUST complete before any user story

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [X] T007 Create Unit struct in `Adom.Framework/src/Adom.CQRS/Unit.cs`
- [X] T008 [P] Create IRequest<TResponse> interface in `Adom.Framework/src/Adom.CQRS/Abstractions/IRequest.cs`
- [X] T009 [P] Create IRequest interface (inherits IRequest<Unit>) in `Adom.Framework/src/Adom.CQRS/Abstractions/IRequest.cs`
- [X] T010 [P] Create RequestHandlerDelegate<TResponse> delegate in `Adom.Framework/src/Adom.CQRS/Abstractions/RequestHandlerDelegate.cs`
- [X] T011 [P] Create IHandler<TRequest, TResponse> interface in `Adom.Framework/src/Adom.CQRS/Abstractions/IHandler.cs`
- [X] T012 [P] Create IHandler<TRequest> interface in `Adom.Framework/src/Adom.CQRS/Abstractions/IHandler.cs`
- [X] T013 [P] Create IDispatcher interface in `Adom.Framework/src/Adom.CQRS/Abstractions/IDispatcher.cs`
- [X] T014 [P] Create IPipelineBehavior<TRequest, TResponse> interface in `Adom.Framework/src/Adom.CQRS/Abstractions/IPipelineBehavior.cs`
- [X] T015 [P] Create ThrowHelper class in `Adom.Framework/src/Adom.CQRS/Internal/ThrowHelper.cs`
- [X] T016 [P] Create HandlerNotFoundException in `Adom.Framework/src/Adom.CQRS/Exceptions/HandlerNotFoundException.cs`
- [X] T017 [P] Create DispatcherException in `Adom.Framework/src/Adom.CQRS/Exceptions/DispatcherException.cs`

**Checkpoint**: All abstractions defined - user story implementation can now begin

---

## Phase 3: User Story 1 - Execute Command Request (Priority: P1) 🎯 MVP

**Goal**: Developers can create command requests and dispatch them to handlers

**Independent Test**: Create a simple CreateUserCommand with handler, dispatch it, verify handler executes and returns result

### Tests for User Story 1

- [X] T018 [P] [US1] Unit test for Dispatcher command dispatch in `Adom.Framework/tests/Adom.CQRS.Tests/DispatcherTests.cs`
- [X] T019 [P] [US1] Unit test for handler discovery in `Adom.Framework/tests/Adom.CQRS.Tests/HandlerDiscoveryTests.cs`
- [X] T020 [P] [US1] Unit test for exception propagation in `Adom.Framework/tests/Adom.CQRS.Tests/DispatcherTests.cs`

### Implementation for User Story 1

- [X] T021 [P] [US1] Create HandlerDescriptor struct in `Adom.Framework/src/Adom.CQRS/Internal/HandlerDescriptor.cs`
- [X] T022 [P] [US1] Create HandlerRegistry with FrozenDictionary in `Adom.Framework/src/Adom.CQRS/Dispatching/HandlerRegistry.cs`
- [X] T023 [US1] Create AssemblyScanner for handler discovery in `Adom.Framework/src/Adom.CQRS/Discovery/AssemblyScanner.cs`
- [X] T024 [US1] Create PipelineBuilder for delegate chain in `Adom.Framework/src/Adom.CQRS/Dispatching/PipelineBuilder.cs`
- [X] T025 [US1] Implement Dispatcher class in `Adom.Framework/src/Adom.CQRS/Dispatching/Dispatcher.cs`
- [X] T026 [US1] Create CqrsBuilder for fluent configuration in `Adom.Framework/src/Adom.CQRS/Configuration/CqrsBuilder.cs`
- [X] T027 [US1] Create ServiceCollectionExtensions (AddAdomCqrs) in `Adom.Framework/src/Adom.CQRS/Extensions/ServiceCollectionExtensions.cs`
- [X] T028 [US1] Add [MethodImpl(AggressiveInlining)] to hot paths in Dispatcher

**Checkpoint**: Command dispatch works - can dispatch IRequest and receive response

---

## Phase 4: User Story 2 - Execute Query Request (Priority: P1)

**Goal**: Developers can create query requests and dispatch them to handlers (completes CQRS pattern)

**Independent Test**: Create a GetUserByIdQuery with handler, dispatch it, verify correct data returned

### Tests for User Story 2

- [X] T029 [P] [US2] Unit test for query dispatch with parameters in `Adom.Framework/tests/Adom.CQRS.Tests/DispatcherTests.cs`
- [X] T030 [P] [US2] Unit test for void command dispatch (IRequest) in `Adom.Framework/tests/Adom.CQRS.Tests/DispatcherTests.cs`
- [X] T031 [P] [US2] Unit test for missing handler exception in `Adom.Framework/tests/Adom.CQRS.Tests/DispatcherTests.cs`

### Implementation for User Story 2

- [X] T032 [US2] Add DispatchAsync<TRequest> overload for void requests in `Adom.Framework/src/Adom.CQRS/Dispatching/Dispatcher.cs`
- [X] T033 [US2] Implement CancellationToken propagation through pipeline in `Adom.Framework/src/Adom.CQRS/Dispatching/Dispatcher.cs`
- [X] T034 [US2] Add null request validation with ThrowHelper in `Adom.Framework/src/Adom.CQRS/Dispatching/Dispatcher.cs`

**Checkpoint**: Full CQRS pattern works - both commands and queries dispatch correctly

---

## Phase 5: User Story 3 - Cache Query Results (Priority: P2)

**Goal**: Query results are automatically cached when implementing ICacheableRequest

**Independent Test**: Execute cacheable query twice, verify handler only invoked once (second call from cache)

### Tests for User Story 3

- [ ] T035 [P] [US3] Unit test for cache hit in `Adom.Framework/tests/Adom.CQRS.Tests/CachingBehaviorTests.cs`
- [ ] T036 [P] [US3] Unit test for cache miss in `Adom.Framework/tests/Adom.CQRS.Tests/CachingBehaviorTests.cs`
- [ ] T037 [P] [US3] Unit test for non-cacheable request bypass in `Adom.Framework/tests/Adom.CQRS.Tests/CachingBehaviorTests.cs`
- [ ] T038 [P] [US3] Unit test for cache unavailability fallback in `Adom.Framework/tests/Adom.CQRS.Tests/CachingBehaviorTests.cs`

### Implementation for User Story 3

- [ ] T039 [P] [US3] Create ICacheableRequest interface in `Adom.Framework/src/Adom.CQRS/Abstractions/ICacheableRequest.cs`
- [ ] T040 [P] [US3] Create CacheOptions configuration in `Adom.Framework/src/Adom.CQRS/Caching/CacheOptions.cs`
- [ ] T041 [US3] Create CacheKeyGenerator with XxHash128 in `Adom.Framework/src/Adom.CQRS/Caching/CacheKeyGenerator.cs`
- [ ] T042 [US3] Implement CachingBehavior pipeline behavior in `Adom.Framework/src/Adom.CQRS/Caching/CachingBehavior.cs`
- [ ] T043 [US3] Register CachingBehavior in CqrsBuilder in `Adom.Framework/src/Adom.CQRS/Configuration/CqrsBuilder.cs`
- [ ] T044 [US3] Add ConfigureCaching method to CqrsBuilder in `Adom.Framework/src/Adom.CQRS/Configuration/CqrsBuilder.cs`
- [ ] T045 [US3] Handle cache unavailability with graceful fallback in `Adom.Framework/src/Adom.CQRS/Caching/CachingBehavior.cs`

**Checkpoint**: Caching works - ICacheableRequest queries are cached automatically

---

## Phase 6: User Story 4 - Configure Cache Behavior (Priority: P3)

**Goal**: Developers can configure cache duration and custom cache keys per request

**Independent Test**: Create requests with different cache durations, verify each expires correctly

### Tests for User Story 4

- [ ] T046 [P] [US4] Unit test for custom cache duration in `Adom.Framework/tests/Adom.CQRS.Tests/CachingBehaviorTests.cs`
- [ ] T047 [P] [US4] Unit test for custom cache key in `Adom.Framework/tests/Adom.CQRS.Tests/CachingBehaviorTests.cs`
- [ ] T048 [P] [US4] Unit test for cache key prefix in `Adom.Framework/tests/Adom.CQRS.Tests/CacheKeyGeneratorTests.cs`

### Implementation for User Story 4

- [ ] T049 [US4] Add CacheKey property support to CacheKeyGenerator in `Adom.Framework/src/Adom.CQRS/Caching/CacheKeyGenerator.cs`
- [ ] T050 [US4] Add KeyPrefix configuration to CacheOptions in `Adom.Framework/src/Adom.CQRS/Caching/CacheOptions.cs`
- [ ] T051 [US4] Add DefaultDuration configuration to CacheOptions in `Adom.Framework/src/Adom.CQRS/Caching/CacheOptions.cs`
- [ ] T052 [US4] Implement DisableCaching method in CqrsBuilder in `Adom.Framework/src/Adom.CQRS/Configuration/CqrsBuilder.cs`

**Checkpoint**: Cache configuration complete - full control over caching behavior

---

## Phase 7: Pipeline Behaviors (Enhancement)

**Goal**: Support custom pipeline behaviors for cross-cutting concerns

**Independent Test**: Create logging behavior, verify it wraps handler execution

### Tests for Pipeline Behaviors

- [ ] T053 [P] Unit test for single behavior execution in `Adom.Framework/tests/Adom.CQRS.Tests/PipelineBehaviorTests.cs`
- [ ] T054 [P] Unit test for behavior chain order in `Adom.Framework/tests/Adom.CQRS.Tests/PipelineBehaviorTests.cs`
- [ ] T055 [P] Unit test for behavior short-circuit in `Adom.Framework/tests/Adom.CQRS.Tests/PipelineBehaviorTests.cs`

### Implementation for Pipeline Behaviors

- [ ] T056 Add AddBehavior<T> method to CqrsBuilder in `Adom.Framework/src/Adom.CQRS/Configuration/CqrsBuilder.cs`
- [ ] T057 Implement behavior ordering in PipelineBuilder in `Adom.Framework/src/Adom.CQRS/Dispatching/PipelineBuilder.cs`
- [ ] T058 Register behaviors as Scoped in DI in `Adom.Framework/src/Adom.CQRS/Extensions/ServiceCollectionExtensions.cs`

**Checkpoint**: Pipeline behaviors work - custom cross-cutting concerns supported

---

## Phase 8: Source Generators (Optional Performance Mode)

**Goal**: Compile-time handler discovery for Native AOT and zero-reflection

### Tests for Source Generators

- [ ] T059 [P] Unit test for generated registration code in `Adom.Framework/tests/Adom.CQRS.Tests/SourceGeneratorTests.cs`

### Implementation for Source Generators

- [ ] T060 Create Adom.CQRS.SourceGenerators.csproj in `Adom.Framework/src/Adom.CQRS.SourceGenerators/Adom.CQRS.SourceGenerators.csproj`
- [ ] T061 Implement HandlerRegistrationGenerator in `Adom.Framework/src/Adom.CQRS.SourceGenerators/HandlerRegistrationGenerator.cs`
- [ ] T062 Add UseGeneratedRegistration method to CqrsBuilder in `Adom.Framework/src/Adom.CQRS/Configuration/CqrsBuilder.cs`
- [ ] T063 Add [DynamicallyAccessedMembers] attributes for AOT in `Adom.Framework/src/Adom.CQRS/Abstractions/IHandler.cs`

**Checkpoint**: Source generators work - zero-reflection registration available

---

## Phase 9: Polish & Cross-Cutting Concerns

**Purpose**: Performance optimization, documentation, and final cleanup

- [ ] T064 [P] Create DispatchBenchmarks in `Adom.Framework/tests/Adom.CQRS.Benchmarks/DispatchBenchmarks.cs`
- [ ] T065 [P] Create CachingBenchmarks in `Adom.Framework/tests/Adom.CQRS.Benchmarks/CachingBenchmarks.cs`
- [ ] T066 Run benchmarks and verify <1ms dispatch overhead
- [ ] T067 [P] Add XML documentation to all public types
- [ ] T068 [P] Configure README.md with quickstart example
- [ ] T069 Verify zero-allocation on hot paths with MemoryDiagnoser
- [ ] T070 Run all tests and verify 100% pass rate
- [ ] T071 Build NuGet package and verify metadata

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup - BLOCKS all user stories
- **User Story 1 (Phase 3)**: Depends on Foundational - MVP deliverable
- **User Story 2 (Phase 4)**: Depends on Foundational - can parallel with US1
- **User Story 3 (Phase 5)**: Depends on Foundational - requires US1/US2 dispatcher
- **User Story 4 (Phase 6)**: Depends on US3 (caching infrastructure)
- **Pipeline Behaviors (Phase 7)**: Depends on Foundational - can parallel after US1
- **Source Generators (Phase 8)**: Depends on Foundational - can parallel after US1
- **Polish (Phase 9)**: Depends on all features being complete

### User Story Dependencies

```
Foundational (Phase 2)
    ├── US1: Command Request (P1) ─┬── US3: Caching (P2) ── US4: Cache Config (P3)
    └── US2: Query Request (P1) ───┘
```

- **US1 & US2**: Both P1, can work in parallel after Foundational
- **US3**: Requires US1/US2 dispatcher infrastructure
- **US4**: Requires US3 caching infrastructure

### Parallel Opportunities

**Within Phase 1 (Setup)**:
```
T002, T003 can run in parallel (different test projects)
T005, T006 can run in parallel (different config files)
```

**Within Phase 2 (Foundational)**:
```
T008-T017 can ALL run in parallel (different files)
```

**Within User Story 1**:
```
T018-T020 tests can run in parallel
T021, T022 can run in parallel (different files)
```

**Across User Stories (after Foundational)**:
```
US1 and US2 can run in parallel (both P1)
Pipeline Behaviors can run in parallel with US1/US2
```

---

## Implementation Strategy

### MVP First (User Stories 1 + 2 Only)

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational (CRITICAL)
3. Complete Phase 3: User Story 1 (Command dispatch)
4. Complete Phase 4: User Story 2 (Query dispatch)
5. **STOP and VALIDATE**: Run all tests, verify CQRS pattern works
6. Deploy MVP NuGet package (v0.1.0)

### Incremental Delivery

1. **v0.1.0**: Setup + Foundational + US1 + US2 → Basic CQRS
2. **v0.2.0**: Add US3 → Caching support
3. **v0.3.0**: Add US4 → Cache configuration
4. **v0.4.0**: Add Pipeline Behaviors → Extensibility
5. **v1.0.0**: Add Source Generators + Polish → Production ready

### Suggested MVP Scope

**Phase 1-4 only** (Tasks T001-T034):
- Project structure ✓
- Core abstractions ✓
- Command dispatch ✓
- Query dispatch ✓

This delivers a functional CQRS library that can be used immediately.

---

## Notes

- [P] tasks = different files, no dependencies
- [US1-US4] labels map tasks to specific user stories
- Each user story is independently testable after completion
- Verify tests fail before implementing
- Commit after each task or logical group
- Stop at any checkpoint to validate story independently
- Run `dotnet build` after each phase to catch issues early
