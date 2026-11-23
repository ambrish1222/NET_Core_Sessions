/* =====================================================================
   Session 9 | Slide 24 | Complete TaskDb schema in ONE script

   Use it to rebuild everything from scratch, or as the "answer key" for the homework.

   !!  WARNING: this DROPS the TaskDb database if it exists, then recreates it.  !!

       TaskDb
       ├── Users
       │   ├── Id (PK)
       │   └── Name
       ├── TaskStatuses (lookup)
       │   ├── Id (PK)
       │   └── Name (UNIQUE)
       └── Tasks
           ├── Id (PK)
           ├── UserId (FK -> Users.Id, nullable)
           ├── Title
           ├── Status (FK -> TaskStatuses.Id)
           ├── Priority (CHECK 1..3)
           ├── DueDate (nullable)
           └── CreatedAt (DEFAULT SYSUTCDATETIME())
   ===================================================================== */
USE master;
GO

IF DB_ID(N'TaskDb') IS NOT NULL
BEGIN
    ALTER DATABASE TaskDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;   -- kick out other connections
    DROP DATABASE TaskDb;
END
GO

CREATE DATABASE TaskDb;
GO

USE TaskDb;
GO

-- ---------- Tables ----------
CREATE TABLE dbo.Users
(
    Id   INT IDENTITY(1,1) CONSTRAINT PK_Users PRIMARY KEY,
    Name NVARCHAR(100)     NOT NULL
);

CREATE TABLE dbo.TaskStatuses
(
    Id   INT          NOT NULL CONSTRAINT PK_TaskStatuses PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL CONSTRAINT UQ_TaskStatuses_Name UNIQUE
);

CREATE TABLE dbo.Tasks
(
    Id        INT IDENTITY(1,1) CONSTRAINT PK_Tasks PRIMARY KEY,
    UserId    INT           NULL     CONSTRAINT FK_Tasks_Users        REFERENCES dbo.Users(Id),
    Title     NVARCHAR(200) NOT NULL,
    Status    INT           NOT NULL CONSTRAINT FK_Tasks_TaskStatuses REFERENCES dbo.TaskStatuses(Id),
    Priority  INT           NOT NULL CONSTRAINT CK_Tasks_Priority     CHECK (Priority BETWEEN 1 AND 3),
    DueDate   DATETIME2     NULL,
    CreatedAt DATETIME2     NOT NULL CONSTRAINT DF_Tasks_CreatedAt    DEFAULT SYSUTCDATETIME()
);
GO

-- ---------- Indexes (for queries the API will run often) ----------
CREATE INDEX IX_Tasks_Status ON dbo.Tasks(Status);   -- "tasks by status"
CREATE INDEX IX_Tasks_UserId ON dbo.Tasks(UserId);   -- "tasks for one user" (FK columns are not auto-indexed)
GO

-- ---------- Reference + sample data ----------
INSERT INTO dbo.TaskStatuses (Id, Name)
VALUES (1, N'Pending'), (2, N'InProgress'), (3, N'Completed');

INSERT INTO dbo.Users (Name)
VALUES (N'Asha Verma'), (N'Rohan Mehta');

INSERT INTO dbo.Tasks (UserId, Title, Status, Priority, DueDate)
VALUES
    (1,    N'Learn SQL Server',          1, 2, NULL),
    (1,    N'Design the Task database',  2, 3, DATEADD(DAY,  3, SYSUTCDATETIME())),
    (1,    N'Write CRUD queries',        1, 2, DATEADD(DAY,  7, SYSUTCDATETIME())),
    (2,    N'Create the Users table',    1, 1, NULL),
    (2,    N'Add a foreign key',         3, 3, DATEADD(DAY, -2, SYSUTCDATETIME())),
    (NULL, N'Create an index',           2, 2, DATEADD(DAY,  5, SYSUTCDATETIME()));
GO

-- ---------- Check the result ----------
SELECT t.Id, u.Name AS Owner, t.Title, s.Name AS Status, t.Priority, t.DueDate, t.CreatedAt
FROM dbo.Tasks AS t
LEFT JOIN dbo.Users        AS u ON u.Id = t.UserId
INNER JOIN dbo.TaskStatuses AS s ON s.Id = t.Status
ORDER BY t.Id;
GO
