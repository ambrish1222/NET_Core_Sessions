/* =====================================================================
   Session 9 | Slides 10, 11 + Exercise 1 | SELECT and WHERE
   Run the statements one at a time (highlight + Execute) and read each result.
   ===================================================================== */
USE TaskDb;
GO

-- 1. Everything (fine while learning; avoid SELECT * in real application code)
SELECT * FROM dbo.Tasks;

-- 2. Only the columns we need
SELECT Id, Title, Status, Priority, DueDate
FROM dbo.Tasks;

-- 3. WHERE limits the rows
SELECT * FROM dbo.Tasks WHERE Status = 1;      -- Pending tasks
SELECT * FROM dbo.Tasks WHERE Id = 1;          -- one task by primary key
SELECT * FROM dbo.Tasks WHERE Priority = 3;    -- High priority

-- 4. Combine conditions with AND / OR
SELECT * FROM dbo.Tasks WHERE Status = 1 AND Priority >= 2;   -- both must be true
SELECT * FROM dbo.Tasks WHERE Status = 2 OR  Priority = 3;    -- either may be true

-- 5. Sorting (NULL DueDates sort first when ascending)
SELECT Id, Title, Priority, DueDate
FROM dbo.Tasks
ORDER BY Priority DESC, DueDate;

-- 6. Counting rows per status
SELECT Status, COUNT(*) AS TaskCount
FROM dbo.Tasks
GROUP BY Status
ORDER BY Status;
GO
