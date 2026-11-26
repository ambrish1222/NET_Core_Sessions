namespace TaskMaster.Session03.Exercises;

/// <summary>Exercise 3 (slide 27): model answers to the design questions. Answer them yourself first.</summary>
public static class ExplainDesignExercise
{
    public static void Run()
    {
        Ask("Why is TaskItem a class?",
            "It groups related data (title, status, due date...) and later behaviour into one meaningful type. One class creates many task objects.");

        Ask("Why is TaskItemStatus an enum?",
            "Status has a small fixed set of values. An enum makes typos impossible at compile time and is more readable than numbers or strings.");

        Ask("Why is ITaskRepository an interface?",
            "It is a contract: it lists the operations (GetAll, GetById, Add...) without saying how they work, so the rest of the code does not depend on the storage.");

        Ask("What is the difference between ITaskRepository and InMemoryTaskRepository?",
            "The interface is the promise (what). The class is one implementation of that promise (how): it keeps tasks in a List in memory.");

        Ask("Why will this design help when we move to SQL Server?",
            "We write a new repository (EF Core) that implements the same interface. Code that depends on ITaskRepository, like TaskService, stays almost unchanged.");
    }

    private static void Ask(string question, string answer)
    {
        Console.WriteLine($"Q: {question}");
        Console.WriteLine($"A: {answer}");
        Console.WriteLine();
    }
}
