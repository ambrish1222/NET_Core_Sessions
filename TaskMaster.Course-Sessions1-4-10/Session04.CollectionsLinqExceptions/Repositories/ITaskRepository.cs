using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Repositories;

/// <summary>Exercise 2 and homework: repository contract with LINQ-based queries.</summary>
public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    void Add(TaskItem task);

    /// <returns>true if a task was deleted, false if no task has that id.</returns>
    bool Delete(int id);

    // Homework queries
    IEnumerable<TaskItem> GetByStatus(TaskItemStatus status);
    IEnumerable<TaskItem> GetByPriority(TaskPriority priority);
    IEnumerable<TaskItem> SearchByTitle(string text);
    IEnumerable<TaskItem> GetSortedByDueDate(bool descending = false);
}
