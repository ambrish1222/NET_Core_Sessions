/* =====================================================================
   Session 9 | Slides 14, 15, 16 + Exercise 3 | Users + Tasks relationship
   One-to-many: one User has many Tasks.  Tasks.UserId -> Users.Id
   ===================================================================== */
USE TaskDb;
GO

-- 1. Users table
IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        Id   INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL
    );
END
GO

-- 2. Add the UserId column to Tasks (NULL = task not assigned to anyone yet)
IF COL_LENGTH(N'dbo.Tasks', N'UserId') IS NULL
BEGIN
    ALTER TABLE dbo.Tasks ADD UserId INT NULL;
END
GO   -- GO is required: the next statements use the new column

-- 3. The foreign key
IF OBJECT_ID(N'dbo.FK_Tasks_Users', N'F') IS NULL
BEGIN
    ALTER TABLE dbo.Tasks
    ADD CONSTRAINT FK_Tasks_Users
    FOREIGN KEY (UserId) REFERENCES dbo.Users(Id);
END
GO

-- 4. Insert two users
IF NOT EXISTS (SELECT 1 FROM dbo.Users)
BEGIN
    INSERT INTO dbo.Users (Name) VALUES (N'Asha Verma'), (N'Rohan Mehta');
END
GO

-- 5. Assign tasks to users (task 6 stays unassigned on purpose)
UPDATE dbo.Tasks
SET UserId = (SELECT Id FROM dbo.Users WHERE Name = N'Asha Verma')
WHERE Id IN (1, 2, 3);

UPDATE dbo.Tasks
SET UserId = (SELECT Id FROM dbo.Users WHERE Name = N'Rohan Mehta')
WHERE Id IN (4, 5);
GO

-- 6. Query tasks for ONE user
SELECT t.Id, t.Title, t.Status, t.Priority
FROM dbo.Tasks AS t
INNER JOIN dbo.Users AS u ON u.Id = t.UserId
WHERE u.Name = N'Asha Verma';

-- 7. Every task with its owner. LEFT JOIN keeps unassigned tasks (Owner = NULL).
SELECT t.Id, t.Title, u.Name AS Owner
FROM dbo.Tasks AS t
LEFT JOIN dbo.Users AS u ON u.Id = t.UserId
ORDER BY t.Id;

-- 8. Tasks per user
SELECT u.Name, COUNT(t.Id) AS TaskCount
FROM dbo.Users AS u
LEFT JOIN dbo.Tasks AS t ON t.UserId = u.Id
GROUP BY u.Name;
GO

-- 9. Exercise 3: try to insert a task for a user that does not exist.
--    The database refuses (error 547, FOREIGN KEY conflict). TRY/CATCH lets the script continue.
BEGIN TRY
    INSERT INTO dbo.Tasks (UserId, Title, Status, Priority, DueDate, CreatedAt)
    VALUES (999, N'Task for a ghost user', 1, 2, NULL, SYSUTCDATETIME());

    PRINT 'Unexpected: the insert succeeded.';
END TRY
BEGIN CATCH
    SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
GO
