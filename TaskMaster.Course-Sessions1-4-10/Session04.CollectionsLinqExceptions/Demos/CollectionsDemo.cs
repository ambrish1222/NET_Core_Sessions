using TaskMaster.Session04.Data;
using TaskMaster.Session04.Models;

namespace TaskMaster.Session04.Demos;

/// <summary>Slides 3-10: List&lt;T&gt;, Dictionary&lt;TKey,TValue&gt; and IEnumerable&lt;T&gt;.</summary>
public static class CollectionsDemo
{
    // Slides 4-6
    public static void ListBasics()
    {
        var tasks = new List<TaskItem>();
        tasks.Add(new TaskItem { Id = 1, Title = "Learn C#" });
        tasks.Add(new TaskItem { Id = 2, Title = "Learn ASP.NET Core" });

        int count = tasks.Count;               // Count is a property on List
        TaskItem first = tasks[0];             // zero-based index
        Console.WriteLine($"count = {count}, first = {first.Title}");

        tasks.Remove(first);
        Console.WriteLine($"after Remove: count = {tasks.Count}, first = {tasks[0].Title}");
    }

    // Slides 7-8
    public static void DictionaryBasics()
    {
        var tasks = new Dictionary<int, TaskItem>();
        tasks[1] = new TaskItem { Id = 1, Title = "Learn C#" };
        tasks[2] = new TaskItem { Id = 2, Title = "Learn LINQ" };

        // TryGetValue: fast lookup by key, no exception when the key is missing
        if (tasks.TryGetValue(1, out var task))
        {
            Console.WriteLine($"Found by key 1: {task.Title}");
        }

        if (!tasks.TryGetValue(99, out _))
        {
            Console.WriteLine("Key 99 is not in the dictionary");
        }

        Console.WriteLine($"ContainsKey(2) = {tasks.ContainsKey(2)}");

        foreach (var pair in tasks)
        {
            Console.WriteLine($"  key {pair.Key} -> {pair.Value.Title}");
        }

        Console.WriteLine();
        Console.WriteLine("Use a Dictionary when you look items up by a unique key a lot.");
        Console.WriteLine("For a simple ordered list of tasks, List<TaskItem> is enough.");
    }

    // Slides 9-10
    public static void EnumerableBasics()
    {
        List<TaskItem> tasks = SampleData.Create();

        // A List can be used wherever an IEnumerable is expected
        IEnumerable<TaskItem> result = tasks;
        foreach (var task in result.Take(3))
        {
            Console.WriteLine($"  {task.Title}");
        }

        Console.WriteLine();
        Console.WriteLine($"List has a Count property : {tasks.Count}");
        Console.WriteLine($"IEnumerable uses Count()  : {result.Count()}   (a LINQ method that walks the sequence)");
        Console.WriteLine("IEnumerable has no Add or Remove: callers can only read and loop.");
        Console.WriteLine("Think of it as: 'a sequence I can enumerate'.");
    }
}
