namespace TaskMaster.Session03.Domain;

/// <summary>
/// Slide 23: the repository contract. It says WHAT can be done, not HOW.
/// TaskService (later sessions) will depend on this interface only.
/// </summary>
public interface ITaskRepository
{
    IEnumerable<TaskItem> GetAll();
    TaskItem? GetById(int id);
    void Add(TaskItem task);
    void Update(TaskItem task);
    void Delete(int id);
}
