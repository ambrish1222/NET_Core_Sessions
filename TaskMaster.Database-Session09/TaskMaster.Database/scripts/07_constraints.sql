/* =====================================================================
   Session 9 | Slides 17, 18 | Constraints
   PRIMARY KEY, FOREIGN KEY, NOT NULL were used earlier. Here: UNIQUE, CHECK, DEFAULT.
   ===================================================================== */
USE TaskDb;
GO

-- 1. Lookup table for task statuses. UNIQUE stops duplicate names.
IF OBJECT_ID(N'dbo.TaskStatuses', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.TaskStatuses
    (
        Id   INT          NOT NULL PRIMARY KEY,      -- we choose the ids (no IDENTITY)
        Name NVARCHAR(50) NOT NULL UNIQUE
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.TaskStatuses)
BEGIN
    INSERT INTO dbo.TaskStatuses (Id, Name)
    VALUES (1, N'Pending'), (2, N'InProgress'), (3, N'Completed');
END
GO

-- 2. CHECK: priority must be 1, 2 or 3
IF OBJECT_ID(N'dbo.CK_Tasks_Priority', N'C') IS NULL
BEGIN
    ALTER TABLE dbo.Tasks
    ADD CONSTRAINT CK_Tasks_Priority
    CHECK (Priority BETWEEN 1 AND 3);
END
GO

-- 3. Extra: tie Tasks.Status to the lookup table, so only real statuses are allowed
IF OBJECT_ID(N'dbo.FK_Tasks_TaskStatuses', N'F') IS NULL
BEGIN
    ALTER TABLE dbo.Tasks
    ADD CONSTRAINT FK_Tasks_TaskStatuses
    FOREIGN KEY (Status) REFERENCES dbo.TaskStatuses(Id);
END
GO

-- 4. DEFAULT: CreatedAt fills itself in when an INSERT does not provide it
IF OBJECT_ID(N'dbo.DF_Tasks_CreatedAt', N'D') IS NULL
BEGIN
    ALTER TABLE dbo.Tasks
    ADD CONSTRAINT DF_Tasks_CreatedAt
    DEFAULT SYSUTCDATETIME() FOR CreatedAt;
END
GO

/* ---------------------------------------------------------------------
   Prove the constraints work. Each statement below SHOULD fail.
   --------------------------------------------------------------------- */

-- CHECK violation: priority 9 (error 547)
BEGIN TRY
    INSERT INTO dbo.Tasks (Title, Status, Priority) VALUES (N'Bad priority', 1, 9);
    PRINT 'Unexpected: the insert succeeded.';
END TRY
BEGIN CATCH
    SELECT 'CHECK' AS Constraint_, ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
END CATCH;

-- FOREIGN KEY violation: status 99 does not exist (error 547)
BEGIN TRY
    INSERT INTO dbo.Tasks (Title, Status, Priority) VALUES (N'Bad status', 99, 2);
    PRINT 'Unexpected: the insert succeeded.';
END TRY
BEGIN CATCH
    SELECT 'FOREIGN KEY' AS Constraint_, ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
END CATCH;

-- UNIQUE violation: a status named "Pending" already exists (error 2627)
BEGIN TRY
    INSERT INTO dbo.TaskStatuses (Id, Name) VALUES (4, N'Pending');
    PRINT 'Unexpected: the insert succeeded.';
END TRY
BEGIN CATCH
    SELECT 'UNIQUE' AS Constraint_, ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
END CATCH;

-- NOT NULL violation: Title is required (error 515)
BEGIN TRY
    INSERT INTO dbo.Tasks (Title, Status, Priority) VALUES (NULL, 1, 2);
    PRINT 'Unexpected: the insert succeeded.';
END TRY
BEGIN CATCH
    SELECT 'NOT NULL' AS Constraint_, ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
END CATCH;
GO

-- DEFAULT in action: no CreatedAt supplied, yet the insert works (one batch, uses @NewId)
DECLARE @NewId INT;

INSERT INTO dbo.Tasks (Title, Status, Priority) VALUES (N'Default demo', 1, 2);
SET @NewId = SCOPE_IDENTITY();

SELECT Id, Title, CreatedAt FROM dbo.Tasks WHERE Id = @NewId;   -- CreatedAt was filled in

DELETE FROM dbo.Tasks WHERE Id = @NewId;                         -- clean up the demo row
GO
