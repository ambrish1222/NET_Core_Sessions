/* =====================================================================
   Session 9 | Slide 6 | Create a Database
   Run against the default "master" database. Safe to run more than once.

   NOTE: "GO" is NOT T-SQL. It is a batch separator understood by SSMS,
   Azure Data Studio and sqlcmd. Each batch is sent to the server separately.
   ===================================================================== */

-- The slide version is simply:  CREATE DATABASE TaskDb;
-- We add a guard so re-running the script does not fail.
IF DB_ID(N'TaskDb') IS NULL
BEGIN
    CREATE DATABASE TaskDb;
END
GO

USE TaskDb;
GO

-- Confirm which database we are now working in
SELECT DB_NAME() AS CurrentDatabase;
GO
