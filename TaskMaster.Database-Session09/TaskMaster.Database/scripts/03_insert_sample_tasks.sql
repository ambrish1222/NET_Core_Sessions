/* =====================================================================
   Session 9 | Slide 9 + Exercise 1 | INSERT: create data
   Inserts 6 tasks with different Status and Priority values.
   Guarded: does nothing if the table already has rows.
   ===================================================================== */
USE TaskDb;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Tasks)
BEGIN
    INSERT INTO dbo.Tasks (Title, Status, Priority, DueDate, CreatedAt)
    VALUES
        (N'Learn SQL Server',          1, 2, NULL,                                   SYSUTCDATETIME()),
        (N'Design the Task database',  2, 3, DATEADD(DAY,  3, SYSUTCDATETIME()),     SYSUTCDATETIME()),
        (N'Write CRUD queries',        1, 2, DATEADD(DAY,  7, SYSUTCDATETIME()),     SYSUTCDATETIME()),
        (N'Create the Users table',    1, 1, NULL,                                   SYSUTCDATETIME()),
        (N'Add a foreign key',         3, 3, DATEADD(DAY, -2, SYSUTCDATETIME()),     SYSUTCDATETIME()),
        (N'Create an index',           2, 2, DATEADD(DAY,  5, SYSUTCDATETIME()),     SYSUTCDATETIME());
END
GO

-- Always verify an INSERT with a SELECT
SELECT * FROM dbo.Tasks;
GO
