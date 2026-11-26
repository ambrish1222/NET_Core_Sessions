# TaskMaster.Api: Session 10 Project

**EF Core Fundamentals** (Week 3, Session 10). The Session 8 API, but the data now lives in **SQL Server** through **Entity Framework Core** instead of a `List<T>` in RAM.

> The controller, service and DTOs are the Session 8 code, almost untouched. Only the repository changed. That is the point of the layered design.

## What changed since Session 8

| Area | Session 8 | Session 10 |
|------|-----------|------------|
| Repository | `InMemoryTaskRepository` (Singleton) | `TaskRepository` using `TaskDbContext` (**Scoped**) |
| New files | | `Data/TaskDbContext.cs`, `Models/User.cs`, `Repositories/TaskRepository.cs` |
| `TaskItem` | Id, Title, Priority, Status, DueDate | + `CreatedAt`, `UserId`, `User` (slide 7) |
| Enum values | `Pending` = 0 | `Pending = 1, InProgress = 2, Completed = 3` (the numbers stored in SQL) |
| `appsettings.json` | nothing | connection string + `Persistence:Provider` |
| `ITaskRepository`, `TasksController` | | **unchanged** |

## Quick start

Requirements: .NET 10 SDK, a SQL Server (LocalDB / Developer / Docker), and the EF tool:

```bash
dotnet tool install --global dotnet-ef --version 10.0.0
```

### 1. Point the app at your SQL Server (slides 11-12)

`appsettings.json` already holds the slide's connection string (Windows authentication, database `TaskDbEf`):

```
Server=localhost;Database=TaskDbEf;Trusted_Connection=True;TrustServerCertificate=True;
```

**Mac / Linux / Docker** (no Windows authentication). Start the optional container, then keep the password out of source control:

```bash
docker compose up -d
dotnet user-secrets set "ConnectionStrings:TaskDb" "Server=localhost,1433;Database=TaskDbEf;User Id=sa;Password=TaskMaster#2026;TrustServerCertificate=True;"
```

User Secrets are stored outside the project folder, so they never reach Git (slide 12).

### 2. Create the database from the model (slides 13-14, Exercise 2)

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Then open `Migrations/<timestamp>_InitialCreate.cs` and **read it** before applying: it is the C# description of `CREATE TABLE Tasks` and `CREATE TABLE Users`.

> Use a **new** database name (`TaskDbEf`). The Session 9 `TaskDb` already has hand-made `Tasks`/`Users` tables, so `database update` would fail with "There is already an object named 'Tasks'".

### 3. Add sample data and run

Run `scripts/sample-data.sql` in SSMS / Azure Data Studio, then:

```bash
dotnet run
```

- `http://localhost:5080/health/db` should say `reachable`
- `http://localhost:5080/swagger` or `TaskMaster.Api.http` for the CRUD endpoints
- Watch the console: in Development every SQL statement EF Core sends is logged (homework: "inspect the generated SQL")

No SQL Server yet? Set `"Persistence": { "Provider": "InMemory" }` in `appsettings.json` and the Session 8 repository is used instead.

## Slide map

| Slides | Topic | Where in the code |
|--------|-------|-------------------|
| 3-4 | What is EF Core, with vs without | `TaskRepository.GetAllAsync`: one LINQ line instead of SQL + parameters + mapping |
| 5 | Packages | `TaskMaster.Api.EfCore.csproj` |
| 6-7 | Entities | `Models/TaskItem.cs`, `Models/User.cs` |
| 8-9 | DbContext, `TaskDbContext` | `Data/TaskDbContext.cs` |
| 10-12 | `AddDbContext`, connection string, secrets | `Program.cs`, `appsettings.json` |
| 13-14 | Migrations | `dotnet ef ...` (generated in `Migrations/`) |
| 15-16 | Read many / one | `TaskRepository.GetAllAsync`, `GetByIdAsync` |
| 17-19 | Add / Update / Delete | `TaskRepository.AddAsync`, `UpdateAsync`, `DeleteAsync` |
| 20-21 | `AsNoTracking`, async | reads use it, writes do not; every call takes a `CancellationToken` |
| 22-24 | Repository architecture and DI | `Program.cs` (`AddScoped<ITaskRepository, TaskRepository>()`) |
| 25 | Common mistakes | checklist below |
| 26-28 | Exercises | solutions below |
| 30 | Homework | checklist below |

## Exercises: where the solutions are

**Exercise 1 (DbContext).** Packages in the csproj, `TaskDbContext` with `DbSet<TaskItem>`, connection string, `AddDbContext` in `Program.cs`. Check with `dotnet build` and `GET /health/db`.

**Exercise 2 (Migration).** Steps above. Verify in SQL Server:

```sql
SELECT * FROM dbo.Tasks;            -- the table EF Core created
SELECT * FROM dbo.__EFMigrationsHistory;   -- EF Core's record of applied migrations
```

**Exercise 3 (Replace the in-memory repository).** `Repositories/TaskRepository.cs` implements `GetAllAsync`, `GetByIdAsync`, `AddAsync`, `UpdateAsync`, `DeleteAsync`. The DI change is one line in `Program.cs`: `AddSingleton<ITaskRepository, InMemoryTaskRepository>()` became `AddScoped<ITaskRepository, TaskRepository>()`.

## Homework checklist (slide 30)

- [ ] API connected to SQL Server (`/health/db` is OK)
- [ ] `InitialCreate` created and applied
- [ ] In-memory repository replaced (already done here: try to rebuild it yourself from slides 15-19 first)
- [ ] Every endpoint in `TaskMaster.Api.http` tested against the database
- [ ] Each change verified with `SELECT` in SQL Server
- [ ] Copied the SQL EF Core logged for one GET and one POST into your notes

## Review quiz: answers (slide 29)

1. **ORM:** Object-Relational Mapper. It maps C# objects to database tables and rows.
2. **DbContext:** EF Core's gateway to the database: one unit of work, exposes `DbSet<T>`s, tracks changes, sends queries and saves updates.
3. **`DbSet<T>`:** the collection of one entity type; it represents a table. You query and add/remove through it.
4. **Migration:** a versioned description of a schema change (create/alter tables, columns, indexes). EF Core records applied ones in `__EFMigrationsHistory`.
5. **`AsNoTracking()`:** for read-only queries. EF Core skips change tracking, which saves memory and time. Do not use it when you intend to modify and save the entity.
6. **`SaveChangesAsync()`:** it is what actually sends INSERT/UPDATE/DELETE to the database. `Add` and `Remove` alone change nothing. Async because it is I/O.
7. **Why pass `CancellationToken`:** if the client disconnects, the database call can stop instead of doing useless work.
8. **Where EF Core code lives:** in the repository layer (`TaskRepository`), behind `ITaskRepository`. Not in controllers or services.
9. **DbContext not registered:** the app fails when the first request needs it: `Unable to resolve service for type 'TaskDbContext'...` (an `InvalidOperationException` from DI).

## Common mistakes (slide 25) and how you would notice

| Mistake | Symptom |
|---------|---------|
| Wrong or missing connection string | startup error from `Program.cs`, or `SqlException` "cannot open database" |
| DbContext not registered | `Unable to resolve service for type ...TaskDbContext` |
| Package versions differ | `MissingMethodException` or build warnings; keep all EF packages on 10.0.x |
| Migrating the wrong project | "Unable to create a DbContext"; use `--project` / `--startup-project` |
| Forgot `SaveChangesAsync()` | request succeeds, table unchanged |
| Loading too much | `SELECT *` for every column and row; project with `Select`, filter with `Where` |
| No `AsNoTracking` on big reads | high memory use |
| No `CancellationToken` | queries keep running after the client left |

## Notes on the deck (what students will hit if they copy slides as-is)

- **`TaskStatus`** in slides 4, 7, 15, 17 clashes with `System.Threading.Tasks.TaskStatus` (CS0104). This project uses `TaskItemStatus`.
- **Slide 23** shows only `GetByIdAsync`; the full repository is in this project.
- **Slide 11** uses Windows authentication (`Trusted_Connection=True`), which does not work on Mac/Linux/Docker. Use SQL authentication through User Secrets (above).
- **Slide 14** is labelled "SQL / DDL" but the two lines are CLI commands (`dotnet ef`), not SQL.
- **Slide 1** header says "Week 2 ... Day 5" while the subtitle says "Week 3". Slide 30 says the next topic is relationships.
- **Slide 24** is correct to use `AddScoped`. In Session 8 the in-memory repository had to be a Singleton because it owned the data; an EF repository holds a scoped `DbContext`, so it must be Scoped.
- **Enum numbers:** EF Core stores enums as integers. Session 9's tables use 1/2/3, so the enums here start at 1. The Session 8 project still has `Pending = 0`; renumber it if you want the two projects to share one database.
- **Update flow:** the service reads the task (via `AsNoTracking`), then the repository reads it again as a tracked entity before saving. It is one extra query, kept so the interface stays unchanged. Good discussion for the next session on performance.

## Not verified yet

This project was written without a .NET SDK available, so it has **not been compiled or run**, and the `Migrations/` folder is not included: you generate it in Exercise 2. If `dotnet build` reports anything, treat it as a bug in this sample.
