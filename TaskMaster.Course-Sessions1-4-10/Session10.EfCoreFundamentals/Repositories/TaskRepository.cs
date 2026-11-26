using Microsoft.EntityFrameworkCore;
using TaskMaster.Api.Data;
using TaskMaster.Api.Models;

namespace TaskMaster.Api.Repositories;

/// <summary>
/// Slides 15-23 and Exercise 3. The EF Core implementation of ITaskRepository.
/// This is the ONLY place that knows about DbContext and SQL Server. Controller and service are unchanged.
/// Every method is async and passes the CancellationToken to EF Core (slide 21).
/// </summary>
public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _db;

    public TaskRepository(TaskDbContext db)
    {
        _db = db;
    }

    // Slide 15: read-only list. AsNoTracking = no change tracking, because we will not modify these.
    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _db.Tasks
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .ToListAsync(cancellationToken);
    }

    // Slides 16 and 23: FirstOrDefaultAsync returns null when nothing matches.
    public async Task<TaskItem?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _db.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    // Slide 17: Add only marks the entity as "to insert". Nothing reaches SQL Server until SaveChangesAsync.
    // After saving, SQL Server's IDENTITY value has been copied back into task.Id.
    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken)
    {
        _db.Tasks.Add(task);
        await _db.SaveChangesAsync(cancellationToken);
    }

    // Slide 18: load a TRACKED entity (no AsNoTracking), change it, save. EF Core notices which properties
    // changed and generates an UPDATE for those columns only.
    public async Task<bool> UpdateAsync(TaskItem task, CancellationToken cancellationToken)
    {
        var existing = await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == task.Id, cancellationToken);

        if (existing is null)
            return false;

        existing.Title = task.Title;
        existing.Priority = task.Priority;
        existing.Status = task.Status;
        existing.DueDate = task.DueDate;
        // Id and CreatedAt are deliberately not copied: they never change after creation.

        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }

    // Slide 19: find, Remove, SaveChangesAsync. Missing task -> false (the controller turns that into 404).
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var task = await _db.Tasks
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (task is null)
            return false;

        _db.Tasks.Remove(task);
        await _db.SaveChangesAsync(cancellationToken);
        return true;
    }
}
