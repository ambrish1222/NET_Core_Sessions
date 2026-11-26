# Session 3: Classes, Objects, Enums & Interfaces

Week 1, Day 3. This is where the Task API gets its first real domain model: `TaskItem`, two enums, `ITaskRepository` and an in-memory implementation.

## Run it

```bash
cd Session03.ClassesEnumsInterfaces
dotnet run                 # interactive menu
dotnet run -- 6 E2         # run item 6 and exercise E2, then exit
dotnet run -- all          # run everything
```

## The domain (reused in later sessions)

```
Domain/
├── TaskItem.cs              Id, Title, Description?, Status, Priority, DueDate
├── TaskItemStatus.cs        Pending, InProgress, Completed
├── TaskPriority.cs          Low = 1, Medium = 2, High = 3
├── ITaskRepository.cs       GetAll, GetById, Add, Update, Delete   (the contract)
├── InMemoryTaskRepository.cs   List<TaskItem> implementation        (the "how")
└── SampleData.cs            5 ready-made tasks
```

## What is in the menu

| Item | Slides | File | Shows |
|------|--------|------|-------|
| 1 | 3 | `Demos/ClassesDemo.cs` | Recap |
| 2 | 4-7, 12 | `Demos/ClassesDemo.cs` | Class vs object, `new`, object initializer |
| 3 | 8-9 | `Demos/ClassesDemo.cs` | Properties (`get`, `set`, read-only, computed) |
| 4 | 10-11 | `Demos/ClassesDemo.cs` | Constructors that guarantee a valid object |
| 5 | 13-17 | `Demos/EnumsDemo.cs` | Enums, why not strings, `Enum.TryParse` |
| 6 | 18-20, 23-24 | `Demos/InterfaceDemo.cs` | Interface vs implementation; swapping implementations |
| 7 | 21-22 | `Demos/TaskModelDemo.cs` | The complete `TaskItem` |
| E1 | 25 | `Exercises/BuildTaskItemExercise.cs` | Three different `TaskItem` objects |
| E2 | 26 | `Exercises/RepositoryExercise.cs` | All repository operations |
| E3 | 27 | `Exercises/ExplainDesignExercise.cs` | Model answers to the five design questions |
| H | 29 | `Homework/HomeworkDemo.cs` | Homework checklist, run live |

> The exercise files are worked solutions. Try each one yourself first.

## Homework checklist (slide 29)

- [x] Finish `InMemoryTaskRepository`: `Add`, `Update`, `Delete`
- [x] Duplicate ids rejected (`Add` throws `InvalidOperationException`)
- [x] `GetById` returns `null` when not found
- [x] At least 5 sample tasks (`SampleData`)
- [ ] Write the flow on paper: `TaskItem -> ITaskRepository -> InMemoryTaskRepository`

## Review quiz: answers (slide 28)

1. **Class**: a blueprint/type that groups related data and behaviour.
2. **Object**: an instance of a class, created with `new`.
3. **Property**: data a class exposes, with `get` and/or `set`.
4. **Constructor**: code that runs when an object is created and sets its initial state.
5. **Object initializer**: `new TaskItem { Id = 1, Title = "..." }`, concise object creation.
6. **Enum**: when a value must come from a small predefined set (Pending, InProgress, Completed).
7. **Interface**: a contract that says what operations exist, not how they work.
8. **Implementation details in an interface?** No: it declares members; the class provides the code.
9. **Interface for a repository**: so the storage can change (memory, SQL Server) without changing the code that uses it.

## Notes

- **`TaskStatus` is called `TaskItemStatus` here.** The deck uses `TaskStatus`, but .NET already has `System.Threading.Tasks.TaskStatus`, which causes a compile error (CS0104) once your types live in namespaces. Same name is used in every later project.
- **`TaskPriority` values start at 1** (`Low = 1`, `Medium = 2`, `High = 3`). That differs from Session 2, where `1` meant High. It also matches the database codes in Session 9.
- `Update` and `Delete` throw `KeyNotFoundException` for unknown ids. Exceptions are covered properly in Session 4.
- Next: Session 4 (collections, LINQ, exceptions).
