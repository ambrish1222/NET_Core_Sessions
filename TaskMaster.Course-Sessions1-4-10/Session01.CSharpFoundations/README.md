# Session 1: C# & .NET Foundations

Week 1, Day 1. A console app that turns every slide of the Session 1 deck into code you can run.

## Run it

Requirements: [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
cd Session01.CSharpFoundations
dotnet run                 # interactive menu
dotnet run -- 5 E3         # run item 5 and exercise E3, then exit
dotnet run -- all          # run everything
```

## What is in the menu

| Item | Slides | File | Shows |
|------|--------|------|-------|
| 1 | 3-6 | `Demos/PlatformDemo.cs` | C# vs .NET, SDK vs Runtime, and the runtime you are running on |
| 2 | 7-9 | `Demos/PlatformDemo.cs` | CLI commands and what Program.cs / .csproj are for |
| 3 | 10 | `Demos/PlatformDemo.cs` | `Console.WriteLine` |
| 4 | 11 | `Demos/VariablesDemo.cs` | Type, name, value |
| 5 | 12 | `Demos/VariablesDemo.cs` | string, int, long, decimal, double, bool, char; why decimal for money |
| 6 | 13 | `Demos/VariablesDemo.cs` | String interpolation `$"..."` |
| 7 | 14 | `Demos/TaskDemo.cs` | `DateTime.UtcNow.AddDays(7)`, `Guid.NewGuid()` |
| 8 | 15 | `Demos/VariablesDemo.cs` | `var` is still strongly typed |
| 9 | 16-18 | `Demos/TaskDemo.cs` | The 6 Task variables printed with `$` |
| E1 | 19 | `Exercises/StudentInfoExercise.cs` | Student information |
| E2 | 20 | `Exercises/TaskInfoExercise.cs` | Task info, change values, explain the types |
| E3 | 21 | `Exercises/ArithmeticExercise.cs` | Arithmetic and integer division |

The project file `Session01.CSharpFoundations.csproj` has the four settings from slide 9 (`OutputType`, `TargetFramework`, `Nullable`, `ImplicitUsings`).

> The exercise files are worked solutions. Try each exercise in your own `dotnet new console` project first, then compare.

## Homework (slide 23)

Build `StudentInfo` and `TaskInfo` console apps from scratch (see E1 and E2). Each needs at least 6 variables, `$` interpolation, `DateTime.UtcNow` and `Guid.NewGuid()`, clear camelCase names and comments. Be ready to explain every data type you chose.

## Review quiz: answers (slide 22)

1. **C#**: a modern, strongly typed, object-oriented language from Microsoft.
2. **.NET**: Microsoft's development and execution platform.
3. **C# vs .NET**: C# is the language; .NET is the runtime, libraries and tools.
4. **SDK**: develop, compile and build applications.
5. **Runtime**: run already-compiled applications.
6. **`dotnet run`**: builds and executes the project in one command.
7. **Variable**: a named memory container with a type, a name and a value.
8. **int vs string**: whole numbers vs text.
9. **bool**: a true/false condition.
10. **Guid for an id**: a globally unique identifier, effectively no collisions.

## Notes

- Session 1 uses `priority = 1` meaning **High** (1 = High, 2 = Medium, 3 = Low), exactly as in the deck. From Session 3 on, priority becomes an enum where `High` is the largest value. Do not mix the two scales.
- Next: Session 2 (decisions, loops, methods, null).
