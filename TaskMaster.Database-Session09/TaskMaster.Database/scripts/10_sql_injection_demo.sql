/* =====================================================================
   Session 9 | Slide 23 | SQL Injection: a first look (read-only demo)

   Rule: NEVER build SQL by concatenating untrusted input.
   Use parameters (ADO.NET / Dapper) or EF Core, which sends parameterized SQL for you.
   ===================================================================== */
USE TaskDb;
GO

-- Imagine this text arrived from a search box in the web app:
DECLARE @UserInput NVARCHAR(100) = N'x'' OR 1 = 1 --';

-- ---------------------------------------------------------------------
-- BAD: concatenation. The input becomes part of the SQL command itself.
-- ---------------------------------------------------------------------
DECLARE @BadSql NVARCHAR(MAX) =
    N'SELECT Id, Title FROM dbo.Tasks WHERE Title = ''' + @UserInput + N'''';

PRINT 'BAD SQL that gets executed:';
PRINT @BadSql;            -- ... WHERE Title = 'x' OR 1 = 1 --'   (the rest is a comment)

EXEC (@BadSql);           -- returns EVERY task: the attacker changed the meaning of the query

-- ---------------------------------------------------------------------
-- GOOD: parameterized. The input is only ever treated as a value, never as SQL.
-- ---------------------------------------------------------------------
EXEC sp_executesql
    N'SELECT Id, Title FROM dbo.Tasks WHERE Title = @Title',
    N'@Title NVARCHAR(100)',
    @Title = @UserInput;  -- returns 0 rows: no task has that literal title
GO

/* In C# the same idea looks like this (shown for reference, not part of this project yet):

   // BAD
   var sql = "SELECT * FROM Tasks WHERE Title = '" + title + "'";

   // GOOD (ADO.NET)
   var cmd = new SqlCommand("SELECT * FROM Tasks WHERE Title = @Title", connection);
   cmd.Parameters.AddWithValue("@Title", title);

   // GOOD (EF Core, next session) - parameters are generated for you
   var tasks = await db.Tasks.Where(t => t.Title == title).ToListAsync();
*/
