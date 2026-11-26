/* =====================================================================
   Session 10 | Exercise 2 | Insert sample data AFTER "dotnet ef database update"
   Run against the database EF Core created (default name: TaskDbEf).
   Same rows as Session 9: Status 1=Pending 2=InProgress 3=Completed, Priority 1=Low 2=Medium 3=High.
   ===================================================================== */
USE TaskDbEf;
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Users)
BEGIN
    INSERT INTO dbo.Users (Name) VALUES (N'Asha Verma'), (N'Rohan Mehta');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Tasks)
BEGIN
    INSERT INTO dbo.Tasks (Title, Status, Priority, DueDate, CreatedAt, UserId)
    VALUES
        (N'Learn SQL Server',         1, 2, NULL,                               SYSUTCDATETIME(), (SELECT Id FROM dbo.Users WHERE Name = N'Asha Verma')),
        (N'Design the Task database', 2, 3, DATEADD(DAY,  3, SYSUTCDATETIME()), SYSUTCDATETIME(), (SELECT Id FROM dbo.Users WHERE Name = N'Asha Verma')),
        (N'Write CRUD queries',       1, 2, DATEADD(DAY,  7, SYSUTCDATETIME()), SYSUTCDATETIME(), (SELECT Id FROM dbo.Users WHERE Name = N'Asha Verma')),
        (N'Create the Users table',   1, 1, NULL,                               SYSUTCDATETIME(), (SELECT Id FROM dbo.Users WHERE Name = N'Rohan Mehta')),
        (N'Add a foreign key',        3, 3, DATEADD(DAY, -2, SYSUTCDATETIME()), SYSUTCDATETIME(), (SELECT Id FROM dbo.Users WHERE Name = N'Rohan Mehta')),
        (N'Create an index',          2, 2, DATEADD(DAY,  5, SYSUTCDATETIME()), SYSUTCDATETIME(), NULL);
END
GO

SELECT * FROM dbo.Tasks;
GO
