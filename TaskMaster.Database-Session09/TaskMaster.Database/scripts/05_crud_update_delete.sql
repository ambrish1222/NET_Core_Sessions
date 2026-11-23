/* =====================================================================
   Session 9 | Slides 9, 11, 12, 13 + Exercise 2 | INSERT, UPDATE, DELETE
   We work on a temporary practice row, so the sample data from script 03
   stays untouched for the later scripts.

   Everything below is ONE batch (no GO), because the variable @NewId only
   lives until the end of its batch.
   ===================================================================== */
USE TaskDb;
GO

-- ---------- INSERT ----------
INSERT INTO dbo.Tasks (Title, Status, Priority, DueDate, CreatedAt)
VALUES (N'Temporary practice task', 1, 2, NULL, SYSUTCDATETIME());

DECLARE @NewId INT = SCOPE_IDENTITY();      -- the Id SQL Server just generated
SELECT * FROM dbo.Tasks WHERE Id = @NewId;

-- ---------- UPDATE (preview first!) ----------
-- Step 1: run a SELECT with the SAME WHERE condition to see which rows will change.
SELECT * FROM dbo.Tasks WHERE Id = @NewId;

-- Step 2: the UPDATE
UPDATE dbo.Tasks
SET Status   = 2,
    Priority = 1
WHERE Id = @NewId;

-- Step 3: verify
SELECT * FROM dbo.Tasks WHERE Id = @NewId;

-- ---------- DELETE (preview first!) ----------
SELECT * FROM dbo.Tasks WHERE Id = @NewId;   -- is this really the row we want to remove?

DELETE FROM dbo.Tasks
WHERE Id = @NewId;

SELECT * FROM dbo.Tasks WHERE Id = @NewId;   -- returns no rows now
GO

/* =====================================================================
   DANGER DEMO: what happens if UPDATE has no WHERE?
   We wrap it in a transaction and ROLLBACK, so nothing is really changed.
   ===================================================================== */
BEGIN TRANSACTION;

    UPDATE dbo.Tasks SET Status = 3;                      -- no WHERE: EVERY row changes!
    SELECT Id, Title, Status FROM dbo.Tasks;              -- all tasks are now "Completed"

ROLLBACK TRANSACTION;                                     -- undo it

SELECT Id, Title, Status FROM dbo.Tasks;                  -- original values are back
GO
