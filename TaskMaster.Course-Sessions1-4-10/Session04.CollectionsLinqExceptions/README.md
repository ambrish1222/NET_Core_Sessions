# Session 4: Collections, LINQ & Exceptions

Week 1, Day 4. Work with many `TaskItem` objects: `List<T>`, `Dictionary`, `IEnumerable<T>`, LINQ and exception handling.

## Run it

```bash
cd Session04.CollectionsLinqExceptions
dotnet run                 # interactive menu
dotnet run -- 4 E1         # run item 4 and exercise E1, then exit
dotnet run -- all          # run everything
```

## Layout

```
Models/         TaskItem, TaskItemStatus, TaskPriority
Data/           SampleData: 10 tasks (one overdue)
Repositories/   ITaskRepository + InMemoryTaskRepository (LINQ inside)
Validation/     TaskValidator: ValidatePriority, ValidateTask (guard clauses)
Demos/          one file per slide group
Exercises/      worked solutions for Exercises 1-3
Homework/       HomeworkDemo: the slide 32 checklist, run live
```

## What is in the menu

| Item | Slides | File | Shows |
|------|--------|------|-------|
| 1 | 4-6 | `Demos/CollectionsDemo.cs` | `List<T>`: Add, Remove, Count, index |
| 2 | 7-8 | `Demos/CollectionsDemo.cs` | `Dictionary`, `TryGetValue`, when (not) to use it |
| 3 | 9-10 | `Demos/CollectionsDemo.cs` | `IEnumerable<T>` vs `List<T>` |
| 4 | 11-13 | `Demos/LinqDemo.cs` | `Where`, combined conditions |
| 5 | 14-15 | `Demos/LinqDemo.cs` | `Select`; Where vs Select |
| 6 | 16 | `Demos/LinqDemo.cs` | `OrderBy`, `OrderByDescending`, `ThenBy` |
| 7 | 17-19 | `Demos/LinqDemo.cs` | `FirstOrDefault`, `Any`, `Count` |
| 8 | 20, 22 | `Demos/LinqDemo.cs` | Chaining and `ToList` |
| 9 | 21-22 | `Demos/DeferredExecutionDemo.cs` | Deferred execution; changing a list inside `foreach` |
| 10 | 23-25 | `Demos/ExceptionsDemo.cs` | `throw`, `try/catch` |
| 11 | 26-27 | `Demos/ExceptionsDemo.cs` | Guard clauses with `ValidateTask` |
| 12 | 23 | `Demos/ExceptionsDemo.cs` | Check expected bad input; do not catch everything |
| E1 | 28 | `Exercises/TaskQueriesExercise.cs` | Eight queries over ten tasks |
| E2 | 29 | `Exercises/RepositoryExercise.cs` | GetAll, GetById, Delete |
| E3 | 30 | `Exercises/ExceptionHandlingExercise.cs` | Validate before add, catch at the boundary |
| H | 32 | `Homework/HomeworkDemo.cs` | Status/priority filters, title search, sorting |

> The exercise files are worked solutions. Try each one yourself first.

## Homework checklist (slide 32)

- [x] In-memory repository completed, `GetById` via `FirstOrDefault`
- [x] Filter by status and priority, search by title, sort by due date (`ITaskRepository`)
- [x] `Delete` with validation (`id <= 0` throws, unknown id returns `false`)
- [x] `ValidateTask` guard clauses
- [x] 10 sample tasks and 5 different LINQ queries (`dotnet run -- H`)

## Review quiz: answers (slide 31)

1. **List vs IEnumerable:** `List<T>` stores items and can be changed (Add/Remove, index). `IEnumerable<T>` is just a sequence you can loop over.
2. **Dictionary:** when you look items up quickly by a unique key.
3. **`Where`:** keeps the items that satisfy a condition (filter).
4. **`Select`:** transforms each item into something else (projection).
5. **Where vs Select:** Where answers "which items?", Select answers "which data?".
6. **`FirstOrDefault` with no match:** the default value, `null` for reference types.
7. **`Any` vs `Count`:** `Any` when you only need yes/no; it stops at the first match.
8. **`ToList`:** runs the query now and stores the results in a new `List<T>`.
9. **Exception:** an error or unexpected condition during execution.
10. **Why not `catch (Exception)` everywhere:** it hides real bugs; catch specific exceptions where you can handle them.

## Notes

- `TaskStatus` from the deck is called `TaskItemStatus` here (clashes with `System.Threading.Tasks.TaskStatus`). `TaskPriority` is `Low = 1, Medium = 2, High = 3`.
- Sample due dates are relative to today, so "overdue" results stay meaningful.
- Next: Session 5 (async/await, `CancellationToken`, dependency injection).
