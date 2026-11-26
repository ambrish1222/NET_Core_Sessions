namespace TaskMaster.Session01.Exercises;

/// <summary>Exercise 3 (slide 21): simple arithmetic and the integer-division trap.</summary>
public static class ArithmeticExercise
{
    public static void Run()
    {
        int a = 20;
        int b = 10;

        Console.WriteLine($"Addition:       {a} + {b} = {a + b}");
        Console.WriteLine($"Subtraction:    {a} - {b} = {a - b}");
        Console.WriteLine($"Multiplication: {a} * {b} = {a * b}");
        Console.WriteLine($"Division:       {a} / {b} = {a / b}");

        // Bonus: change b to 3. Integer division throws away the decimals.
        int c = 3;
        Console.WriteLine();
        Console.WriteLine($"Integer division: {a} / {c} = {a / c}      (decimals are cut off!)");
        Console.WriteLine($"Remainder:        {a} % {c} = {a % c}");

        double x = 20.0;
        double y = 3.0;
        Console.WriteLine($"Precise division: {x} / {y} = {x / y:F2}   (use double to keep decimals)");
    }
}
