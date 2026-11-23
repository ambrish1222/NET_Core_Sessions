# Database ↔ API mapping (slide 25)

How the SQL schema from this session becomes the C# model in the next session (EF Core).

| SQL world | C# world |
|-----------|----------|
| Table `Tasks` | Class `TaskItem` |
| Column | Property |
| Row | One `TaskItem` object |
| Primary key `Id` | Property `Id` |
| Foreign key `Tasks.UserId → Users.Id` | `UserId` property + navigation property `User` |
| `SELECT` result | Entities (or DTOs after mapping) |
| `NULL` allowed | Nullable type (`int?`, `DateTime?`, `string?`) |

## Column types

| SQL Server | C# |
|------------|----|
| `INT` | `int` |
| `NVARCHAR(n)` | `string` |
| `DATETIME2` | `DateTime` |
| `... NULL` | add `?` (for example `DateTime? DueDate`) |

## Entities that match `TaskDb`

```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // One user has many tasks
    public List<TaskItem> Tasks { get; set; } = new();
}

public class TaskItem
{
    public int Id { get; set; }
    public int? UserId { get; set; }            // FK column, nullable (UserId INT NULL)
    public User? User { get; set; }             // navigation property
    public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime? DueDate { get; set; }      // DueDate DATETIME2 NULL
    public DateTime CreatedAt { get; set; }
}
```

## Heads-up: enum values must match the database codes

This database stores `Status` as 1 = Pending, 2 = InProgress, 3 = Completed, and `Priority` as 1 = Low, 2 = Medium, 3 = High (enforced by the lookup table and the CHECK constraint).

In the Session 8 API project, `TaskPriority` already uses 1, 2, 3, but `TaskItemStatus` does not set values, so C# numbers it 0, 1, 2. Before connecting the API to this database, give the enum explicit values:

```csharp
public enum TaskItemStatus
{
    Pending = 1,
    InProgress = 2,
    Completed = 3
}
```

Otherwise a `Pending` task would be saved as `0`, and the foreign key to `TaskStatuses` would reject it.

## Parameterized queries (slide 23)

```csharp
// BAD: user input becomes part of the SQL text
var sql = "SELECT * FROM Tasks WHERE Title = '" + title + "'";

// GOOD (ADO.NET)
var cmd = new SqlCommand("SELECT * FROM Tasks WHERE Title = @Title", connection);
cmd.Parameters.AddWithValue("@Title", title);

// GOOD (EF Core, next session): parameters are generated for you
var tasks = await db.Tasks.Where(t => t.Title == title).ToListAsync();
```

## Connection string for next session

```
Server=localhost,1433;Database=TaskDb;User Id=sa;Password=TaskMaster#2026;TrustServerCertificate=True
```

(LocalDB on Windows: `Server=(localdb)\MSSQLLocalDB;Database=TaskDb;Trusted_Connection=True;`)
