# TaskMaster.Database: Session 9 Project

**SQL Server Fundamentals & Database Design** (Week 3, Day 4, Session 9)

A hands-on database project for the Task API. The scripts follow the Session 9 slides in order: create `TaskDb`, design the tables, run CRUD queries, add relationships, constraints and indexes, and meet NULL and SQL injection. There is no C# in this session; the next session (EF Core) connects the API to this database.

## 1. Get a SQL Server (choose one)

| Option | Setup |
|--------|-------|
| **Docker** (any OS) | `docker compose up -d` in this folder. Server: `localhost,1433`, user `sa`, password `TaskMaster#2026` |
| **SQL Server LocalDB** (Windows, comes with Visual Studio) | Server: `(localdb)\MSSQLLocalDB`, Windows authentication |
| **SQL Server Developer / Express** | Server: `localhost` or `.\SQLEXPRESS` |

Then connect with **SSMS** or **Azure Data Studio**. For a Docker server, tick *Trust server certificate* when connecting.

## 2. Run the scripts

Open each file in `scripts/` in order. Run a whole script with Execute (F5), or highlight statements to run them one at a time. Most scripts can be re-run safely.

| Script | Slide(s) | What it teaches |
|--------|----------|-----------------|
| `01_create_database.sql` | 6 | `CREATE DATABASE`, `USE`, `GO` |
| `02_create_tasks_table.sql` | 5, 7, 8 | Table, columns, data types, identity primary key |
| `03_insert_sample_tasks.sql` | 9 | `INSERT`, 6 sample tasks (**Exercise 1**) |
| `04_select_and_where.sql` | 10, 11 | `SELECT`, `WHERE`, `AND`/`OR`, `ORDER BY`, `GROUP BY` |
| `05_crud_update_delete.sql` | 9, 11-13 | `UPDATE`, `DELETE`, preview with SELECT first (**Exercise 2**) |
| `06_users_and_foreign_key.sql` | 14-16 | Users table, foreign key, joins, one-to-many (**Exercise 3**) |
| `07_constraints.sql` | 17, 18 | `UNIQUE`, `CHECK`, `DEFAULT`, lookup table, watching constraints reject bad data |
| `08_indexes.sql` | 19, 20 | Index on 50,000 rows: compare scan vs seek |
| `09_null_checks.sql` | 21, 22 | `NULL` vs `''` vs `0`, `IS NULL` |
| `10_sql_injection_demo.sql` | 23 | Concatenation (bad) vs parameters (good), read-only demo |
| `11_task_database_complete.sql` | 24 | Whole schema in one script. **Drops and recreates TaskDb.** |

Run **01 to 07 in order**. Scripts 08 to 10 are demos that need 01 to 07 done first. Script 11 is the answer key for the homework, or a way to reset everything.

### Command line alternative

```bash
sqlcmd -S localhost,1433 -U sa -P "TaskMaster#2026" -C -i scripts/01_create_database.sql
```

(`-C` trusts the dev certificate. For LocalDB use `-S "(localdb)\MSSQLLocalDB" -E` instead of `-U`/`-P`.)

## The schema (slide 24)

```
TaskDb
├── Users
│   ├── Id (PK)
│   └── Name
├── TaskStatuses (lookup: 1 Pending, 2 InProgress, 3 Completed)
│   ├── Id (PK)
│   └── Name (UNIQUE)
└── Tasks
    ├── Id (PK)
    ├── UserId (FK → Users.Id, nullable)
    ├── Title
    ├── Status (FK → TaskStatuses.Id)
    ├── Priority (CHECK 1 to 3: 1 Low, 2 Medium, 3 High)
    ├── DueDate (nullable)
    └── CreatedAt (DEFAULT SYSUTCDATETIME())
```

## Lab exercises (slides 26-28)

Scripts 03, 05 and 06 contain the worked answers; try each exercise yourself first.

1. **Create TaskDb:** create the database and the Tasks table, insert at least 5 tasks with different Status and Priority, then filter by Status and Priority.
2. **CRUD SQL:** select all tasks, select one by Id, insert, update a task's Status, delete one. Before every UPDATE or DELETE, run a SELECT with the same WHERE.
3. **Relationships:** create Users, add the foreign key `Tasks.UserId → Users.Id`, insert two users, assign tasks, query one user's tasks, then try inserting a task with a non-existent UserId and read the error.

Stretch ideas:
- Write a query for overdue tasks (`DueDate < SYSUTCDATETIME()` and not Completed).
- Find users who have no tasks (`LEFT JOIN` ... `WHERE t.Id IS NULL`).
- Add a `Description NVARCHAR(1000) NULL` column with `ALTER TABLE`.
- Create an index for a query you expect the API to run often (hint: tasks for one user).

## Homework (slide 30)

Create TaskDb with Users and Tasks, add the primary and foreign keys, insert sample data, write SELECT / INSERT / UPDATE / DELETE queries, add one index for a frequent query, and **bring your schema to the next session**. `11_task_database_complete.sql` is the reference.

## Review quiz (slide 29)

Questions and answers are in `docs/quiz-answers.md` (trainer copy).

## Common mistakes

- **`UPDATE` or `DELETE` without `WHERE`:** it changes every row. Preview with a SELECT first (see script 05).
- **`WHERE DueDate = NULL`:** returns nothing. Use `IS NULL`.
- **Forgetting `GO`:** `ALTER TABLE ... ADD UserId` followed by a statement using `UserId` in the same batch fails with "Invalid column name". `GO` splits batches.
- **Running scripts out of order:** later scripts expect the tables and rows from earlier ones. Use script 11 to reset.
- **Gaps in `Id` values:** normal. Identity counters never go backwards after a delete or rollback.
- **Building SQL with string concatenation:** see script 10.

## Next session

EF Core: `DbContext`, entities and migrations. See `docs/database-to-api-mapping.md` for how these tables become C# classes, and for one change the Session 8 enums need first.
