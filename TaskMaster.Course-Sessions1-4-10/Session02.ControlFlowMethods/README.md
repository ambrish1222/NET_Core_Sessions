# Session 2: Control Flow, Methods & Nullability

Week 1, Day 2. Decisions, loops, methods and null handling, all runnable from one menu.

## Run it

```bash
cd Session02.ControlFlowMethods
dotnet run                 # interactive menu
dotnet run -- 4 E1         # run item 4 and exercise E1, then exit
dotnet run -- all          # run everything
```

## What is in the menu

| Item | Slides | File | Shows |
|------|--------|------|-------|
| 1-2 | 3-5 | `Demos/DecisionsDemo.cs` | Recap, `== != > < >= <=`, `&& \|\| !` |
| 3 | 6-7 | `Demos/DecisionsDemo.cs` | `if / else if / else` |
| 4 | 8 | `Demos/DecisionsDemo.cs` | `switch` expression |
| 5-6 | 9-10 | `Demos/LoopsDemo.cs` | `for` and `foreach` |
| 7 | 11-15 | `Demos/MethodsDemo.cs` | Methods with/without parameters and return values |
| 8-11 | 16-21 | `Demos/NullDemo.cs` | `string?`, `??`, `?.`, null vs `""`, title validation |
| E1 | 22 | `Exercises/PriorityLabelExercise.cs` | `GetPriorityLabel` with a switch expression |
| E2 | 23 | `Exercises/TaskStatusExercise.cs` | Completed / pending / urgent warning |
| E3 | 24 | `Exercises/ProcessTasksExercise.cs` | `List<string>` with `foreach` and `for` |
| E4 | 25 | `Exercises/ValidationExercise.cs` | `IsValidTitle` tested with null, `""`, `"   "`, a real title |
| H | 27 | `Homework/` | `TaskValidator` and a run over 7 sample tasks |

> The exercise files are worked solutions. Try each one yourself first.

## Homework checklist (slide 27)

- `TaskValidator` with title and priority (1-3) validation: `Homework/TaskValidator.cs`
- `GetPriorityLabel()` and `GetTaskMessage()`: same file
- Process at least 5 tasks with `foreach`, with tests for null and invalid input: `Homework/HomeworkDemo.cs`

## Review quiz: answers (slide 26)

1. `==` returns a `bool` (true if both sides are equal).
2. `&&` needs both sides true; `||` needs at least one side true.
3. `switch` when one value maps to several known cases.
4. `for` when you need an index or a known number of repetitions; `foreach` to process every item.
5. Methods remove duplication and make code readable and testable.
6. `return` sends a value back to the caller and ends the method.
7. `null` means there is no value/reference.
8. `null` is "no string at all"; `""` is a real string with zero characters.
9. `??` supplies a fallback when the left side is null.
10. `?.` accesses a member only when the value is not null (otherwise the result is null).

## Notes

- **Priority scale:** this session uses `1 = High, 2 = Medium, 3 = Low` exactly as the deck does. From Session 3 on, priority is the `TaskPriority` enum (`Low = 1`, `Medium = 2`, `High = 3`), so the numbers mean the opposite. Point this out to learners.
- Next: Session 3 (classes, objects, enums, interfaces).
