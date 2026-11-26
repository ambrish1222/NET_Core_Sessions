using TaskMaster.Session04.Data;
using TaskMaster.Session04.Models;
using TaskMaster.Session04.Repositories;

namespace TaskMaster.Session04.Homework;

/// <summary>Slide 32: ten sample tasks and five different LINQ queries through the repository.</summary>
public static class HomeworkDemo
{
    public static void Run()
    {
        ITaskRepository repository = new InMemoryTaskRepository();
        foreach (var task in SampleData.Create())
        {
            repository.Add(task);
        }
        Console.WriteLine($"Loaded {repository.GetAll().Count()} sample tasks.");

        Print("Query 1: GetByStatus(Pending)", repository.GetByStatus(TaskItemStatus.Pending));
        Print("Query 2: GetByPriority(High)", repository.GetByPriority(TaskPriority.High));
        Print("Query 3: SearchByTitle(\"test\")", repository.SearchByTitle("test"));
        Print("Query 4: GetSortedByDueDate() (earliest first)", repository.GetSortedByDueDate().Take(5));
        Print("Query 5: GetSortedByDueDate(descending) (latest first)", repository.GetSortedByDueDate(descending: true).Take(5));

        Console.WriteLine();
        Console.WriteLine($"GetById(7) uses FirstOrDefault: {repository.GetById(7)?.Title ?? "not found"}");
        Console.WriteLine($"Delete(7): {repository.Delete(7)}, then Delete(7) again: {repository.Delete(7)}");
    }

    private static void Print(string heading, IEnumerable<TaskItem> items)
    {
        Console.WriteLine();
        Console.WriteLine(heading);
        foreach (var item in items)
        {
            Console.WriteLine($"   {item}");
        }
    }
}
