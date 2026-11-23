# Review quiz: trainer answer key (slide 29)

1. **What is a relational database?**
   Data stored in tables (rows and columns) that are related to each other through keys, and queried with SQL.

2. **What is a primary key?**
   A column (or columns) that uniquely identifies each row. It must be unique, not NULL and stable. Example: `Tasks.Id`.

3. **What is a foreign key?**
   A column that references the key of another table (`Tasks.UserId → Users.Id`). The database refuses values that do not exist in the referenced table.

4. **What is the difference between NULL and an empty string?**
   NULL means unknown / not provided. `''` is a real value that happens to be empty. Neither equals 0. Test NULL with `IS NULL`, never `= NULL`.

5. **Why can indexes improve performance?**
   An index is a sorted structure, so SQL Server can seek straight to matching rows instead of scanning the whole table. (Script 08 shows the drop in logical reads.)

6. **Why can too many indexes be harmful?**
   Each index uses storage and must be updated on every INSERT, UPDATE and DELETE, which slows writes. Index based on real query patterns.

7. **What happens if UPDATE has no WHERE?**
   Every row in the table is changed. Preview with a SELECT using the same WHERE first, and use a transaction when unsure (script 05 shows this).

8. **How does a SQL table map to a C# entity?**
   Table → class, column → property, row → object, primary key → `Id`, foreign key → navigation property, nullable column → nullable type.

9. **What is SQL injection?**
   Untrusted input that becomes part of the SQL command text and changes what the query does. Prevent it with parameterized queries or EF Core (script 10 shows the difference).
