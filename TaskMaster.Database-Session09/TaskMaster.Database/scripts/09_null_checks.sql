/* =====================================================================
   Session 9 | Slides 21, 22 | NULL vs empty vs 0
   NULL = unknown / not provided.   '' = a real (empty) text value.   0 = a real number.
   ===================================================================== */
USE TaskDb;
GO

-- 1. NULL is not equal to anything, not even to NULL. The comparison is UNKNOWN, not TRUE.
SELECT
    CASE WHEN NULL = NULL THEN 'equal' ELSE 'NOT true (unknown)' END AS NullEqualsNull,
    CASE WHEN N'' = N''   THEN 'equal' ELSE 'not equal'          END AS EmptyEqualsEmpty,
    CASE WHEN NULL IS NULL THEN 'yes'  ELSE 'no'                 END AS NullIsNull,
    CASE WHEN N'' IS NULL  THEN 'yes'  ELSE 'no'                 END AS EmptyIsNull,
    CASE WHEN 0 IS NULL    THEN 'yes'  ELSE 'no'                 END AS ZeroIsNull;
GO

-- 2. WRONG: "= NULL" never matches anything (returns 0 rows)
SELECT * FROM dbo.Tasks WHERE DueDate = NULL;
GO

-- 3. CORRECT: IS NULL / IS NOT NULL
SELECT * FROM dbo.Tasks WHERE DueDate IS NULL;        -- tasks with no due date
SELECT * FROM dbo.Tasks WHERE DueDate IS NOT NULL;    -- tasks that have a due date
GO

-- 4. Handling NULL in results
SELECT Id,
       Title,
       ISNULL(CONVERT(NVARCHAR(30), DueDate, 120), N'(no due date)') AS DueDateText
FROM dbo.Tasks;
GO

-- 5. Why it matters for the API:  DueDate DATETIME2 NULL  <->  DateTime? DueDate  in C#
