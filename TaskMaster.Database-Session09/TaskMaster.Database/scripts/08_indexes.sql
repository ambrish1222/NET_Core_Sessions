/* =====================================================================
   Session 9 | Slides 19, 20 | Indexes: see the difference with your own eyes

   Plan:
     1. Generate 50,000 "Completed" tasks, so Pending tasks are rare (a selective filter).
     2. Run the Status query WITHOUT an index  -> table scan.
     3. Create IX_Tasks_Status.
     4. Run the same query again               -> index seek, far fewer reads.
     5. Clean up the generated rows.

   How to compare: look at "logical reads" in the Messages tab (SET STATISTICS IO),
   or turn on "Include Actual Execution Plan" in SSMS / Azure Data Studio.
   ===================================================================== */
USE TaskDb;
GO

-- 1. Generate data (skipped if already generated)
IF NOT EXISTS (SELECT 1 FROM dbo.Tasks WHERE Title = N'Generated task 1')
BEGIN
    ;WITH Numbers AS
    (
        SELECT TOP (50000) ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS n
        FROM sys.all_objects AS a
        CROSS JOIN sys.all_objects AS b
    )
    INSERT INTO dbo.Tasks (Title, Status, Priority, DueDate, CreatedAt)
    SELECT N'Generated task ' + CAST(n AS NVARCHAR(10)),
           3,                         -- Completed
           (n % 3) + 1,               -- priority cycles 1, 2, 3
           NULL,
           SYSUTCDATETIME()
    FROM Numbers;
END
GO

SELECT COUNT(*) AS TotalTasks FROM dbo.Tasks;
GO

-- Make sure we start without the index
DROP INDEX IF EXISTS IX_Tasks_Status ON dbo.Tasks;
GO

SET STATISTICS IO ON;
GO

-- 2. WITHOUT an index: SQL Server must scan every row (note "logical reads")
SELECT Id, Title, Status
FROM dbo.Tasks
WHERE Status = 1;
GO

-- 3. Create the index from the slide
CREATE INDEX IX_Tasks_Status
ON dbo.Tasks(Status);
-- Useful when frequently filtering by Status
GO

-- 4. SAME query again: SQL Server can now seek straight to the Pending rows.
--    Expect far fewer logical reads than in step 2.
SELECT Id, Title, Status
FROM dbo.Tasks
WHERE Status = 1;
GO

SET STATISTICS IO OFF;
GO

-- 5. Clean up the generated rows (identity values keep counting: gaps in Id are normal)
DELETE FROM dbo.Tasks
WHERE Title LIKE N'Generated task %';
GO

SELECT COUNT(*) AS TasksAfterCleanup FROM dbo.Tasks;
GO

/* Think about it:
   - Why would an index on Status help much less if most tasks were Pending?
   - Every INSERT/UPDATE/DELETE must also maintain each index. What does that cost?
   - Foreign key columns (Tasks.UserId) are NOT indexed automatically in SQL Server.
     Script 11 adds IX_Tasks_UserId because "tasks for one user" is a frequent query. */
