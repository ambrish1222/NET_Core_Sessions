namespace TaskMaster.Session01.Exercises;

/// <summary>
/// Exercise 1 (slide 19) and Homework task 1: Student Information.
/// Try it yourself first: create a console app with 'dotnet new console -n StudentInfo'.
/// </summary>
public static class StudentInfoExercise
{
    public static void Run()
    {
        Guid studentId = Guid.NewGuid();
        string name = "Priya Sharma";
        int age = 22;
        string course = "ASP.NET Core Web API";
        bool isWorking = false;
        int experience = 0;                    // years of experience
        DateTime joiningDate = DateTime.UtcNow;

        Console.WriteLine($"ID:         {studentId}");
        Console.WriteLine($"Name:       {name} ({age} yrs)");
        Console.WriteLine($"Course:     {course}");
        Console.WriteLine($"Working:    {isWorking}");
        Console.WriteLine($"Experience: {experience} years");
        Console.WriteLine($"Joined:     {joiningDate}");
    }
}
