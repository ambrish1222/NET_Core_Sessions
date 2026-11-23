# TaskMaster.Api: Session 8 Project

**Controllers, Services, Mapping & Complete API Flow** (Week 2, Day 3, Session 8)

A runnable ASP.NET Core Web API that follows the Session 8 slides: a **thin controller**, an **application service**, an **in-memory repository**, **manual DTO mapping**, and complete **GET / POST / PUT / DELETE** flows.

## Quick start

Requirements: [.NET 8 SDK](https://dotnet.microsoft.com/download)

```bash
cd TaskMaster.Api
dotnet run
```

Open **http://localhost:5080/swagger**, or run the requests in `TaskMaster.Api.http` (VS Code REST Client / Visual Studio).

## Project structure

```
TaskMaster.Api/
├── Controllers/   TasksController.cs           HTTP only (slides 5-6)
├── Services/      ITaskService, TaskService    Application logic + Map() (slides 7-9, 18-19)
├── Repositories/  ITaskRepository,             Persistence (slides 10-11)
│                  InMemoryTaskRepository
├── DTOs/          CreateTaskRequest,           API contracts (slide 13)
│                  UpdateTaskRequest,
│                  TaskResponse
├── Models/        TaskItem, TaskPriority,      Domain model
│                  TaskItemStatus
├── Middleware/    ExceptionHandlingMiddleware  ArgumentException -> 400 (preview of slide 24)
└── Program.cs     DI registration + pipeline   (slides 22-23)
```

## The layers

```
HTTP Request
   ↓
TasksController   → reads input, calls the service, picks the HTTP response
   ↓
TaskService       → validates rules, applies changes, maps TaskItem ↔ DTOs
   ↓
ITaskRepository   → find / add / update / delete (InMemoryTaskRepository today, EF Core in Week 3)
   ↓
Data store (List<TaskItem>)
```

Dependency graph (slide 23): `TasksController → ITaskService → TaskService → ITaskRepository → InMemoryTaskRepository`. ASP.NET Core DI builds it; nobody calls `new TaskService()`.

## Endpoints

| Method | URL | Success | Failure |
|--------|-----|---------|---------|
| GET | `/api/tasks` | 200 + list | |
| GET | `/api/tasks/{id}` | 200 + task | 404 |
| POST | `/api/tasks` | 201 + `Location` header + task | 400 |
| PUT | `/api/tasks/{id}` | 204 No Content | 400 (rule broken), 404 |
| DELETE | `/api/tasks/{id}` | 204 No Content | 404 |

## Who decides what?

| Situation | Decided by | Result |
|-----------|-----------|--------|
| `title` is empty / `priority: 9` | Model validation (`[ApiController]` + attributes on the DTO) | 400, service never runs |
| Due date in the past | `TaskService.CreateAsync` | `ArgumentException` → middleware → 400 |
| Completed task moved back to Pending | `TaskService.UpdateAsync` | `ArgumentException` → middleware → 400 |
| Task id not found | Repository finds nothing → service returns `null`/`false` | Controller returns 404 |

The service still checks the title even though model validation catches it first. The service must be safe when it is called from somewhere other than HTTP, such as a unit test or another service.

## Lab exercises (slides 26-28)

| Exercise | Where to look / what to do |
|----------|----------------------------|
| **1. Build the layers** | Find the 4 types (`ITaskService`, `TaskService`, `ITaskRepository`, `InMemoryTaskRepository`) and the 2 registrations in `Program.cs`. |
| **2. Complete GET** | Trace `GET /api/tasks/1` through all layers with the debugger (breakpoints in controller, service, repository). |
| **3. Complete CRUD** | Study `CreateAsync`, `UpdateAsync`, `DeleteAsync` in the service, then run requests 4-15 in the `.http` file. |

Stretch ideas:
1. Add a rule: a task with `High` priority must have a due date.
2. Add a rule: an `InProgress` task cannot be deleted (put it in `DeleteAsync`, not the controller).
3. Move `Map` into a `TaskMappingExtensions` class so you can write `task.ToDto()` and `request.ToEntity()`.
4. Write a second `ITaskRepository` (for example storing to a JSON file) and change one line in `Program.cs`.

## Review quiz (slide 29)

1. What should a controller / service / repository do?
2. Why must the service not return `IActionResult`?
3. Why use interfaces?
4. What is mapping, and why use DTOs?
5. Where will SQL/EF Core code live eventually?
6. Why pass `CancellationToken` through every layer? (See the `ThrowIfCancellationRequested()` calls in the repository.)

## Things that commonly go wrong

- **Repository registered as `AddScoped`:** the in-memory list is recreated on every request, so a POSTed task is gone by the next GET. This project uses `AddSingleton` for the repository until it talks to a real database.
- **`CreatedAtAction(nameof(GetByIdAsync), ...)` throws "No route matches":** ASP.NET Core strips the `Async` suffix from action names by default. `Program.cs` sets `SuppressAsyncSuffixInActionNames = false` to prevent that.
- **Enum named `TaskStatus`:** it collides with `System.Threading.Tasks.TaskStatus` (compile error CS0104). This project calls it `TaskItemStatus`.
- **Returning `TaskItem` from the controller:** this leaks internal fields. Always return a DTO.
- **`try/catch` in every action:** let the middleware handle known exceptions.

## Homework (slide 30)

Finish and test all CRUD endpoints, draw the dependency flow on paper, and be ready to swap the in-memory repository for EF Core in Week 3. Only `InMemoryTaskRepository` and one line of `Program.cs` should need to change.
