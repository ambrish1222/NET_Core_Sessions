namespace TaskMaster.Session01.Demos;

/// <summary>Slides 11-15: variables, common types, string interpolation, var.</summary>
public static class VariablesDemo
{
    // Slide 11: Type variableName = value;
    public static void Declaring()
    {
        string name = "Rahul";
        int age = 22;
        bool isWorking = true;

        Console.WriteLine(name);
        Console.WriteLine(age);
        Console.WriteLine(isWorking);
    }

    // Slide 12
    public static void DataTypes()
    {
        string text = "Learn C#";
        int whole = 25;
        long big = 9876543210L;
        decimal money = 99.99m;
        double science = 3.14159;
        bool flag = true;
        char letter = 'A';

        Console.WriteLine($"string  : {text}");
        Console.WriteLine($"int     : {whole}   (range {int.MinValue} to {int.MaxValue})");
        Console.WriteLine($"long    : {big}");
        Console.WriteLine($"decimal : {money}   (use for money)");
        Console.WriteLine($"double  : {science}");
        Console.WriteLine($"bool    : {flag}");
        Console.WriteLine($"char    : {letter}   (single quotes)");
        Console.WriteLine();
        Console.WriteLine($"Size in bytes -> int:{sizeof(int)} long:{sizeof(long)} double:{sizeof(double)} decimal:{sizeof(decimal)} bool:{sizeof(bool)} char:{sizeof(char)}");

        // Why decimal for money?
        double d1 = 0.1, d2 = 0.2;
        decimal m1 = 0.1m, m2 = 0.2m;
        Console.WriteLine();
        Console.WriteLine($"double : 0.1 + 0.2 == 0.3 ?  {d1 + d2 == 0.3}   (tiny rounding error)");
        Console.WriteLine($"decimal: 0.1 + 0.2 == 0.3 ?  {m1 + m2 == 0.3m}   (exact)");
    }

    // Slide 13
    public static void Interpolation()
    {
        string name = "Rahul";
        int age = 22;

        Console.WriteLine("My name is " + name + " and I am " + age + " years old.");   // clunky
        Console.WriteLine($"My name is {name} and I am {age} years old.");              // elegant
        Console.WriteLine($"Next year, I will be {age + 1} years old.");                // expressions work
        Console.WriteLine($"Half of pi with 2 decimals: {Math.PI / 2:F2}");             // formatting
    }

    // Slide 15
    public static void VarKeyword()
    {
        var name = "Rahul";   // the compiler infers string
        var age = 22;         // the compiler infers int

        Console.WriteLine($"name is a {name.GetType().Name}, age is an {age.GetType().Name}");

        // name = 99;   // COMPILE ERROR: var does NOT mean "no type". The type is locked.
        Console.WriteLine("Uncomment 'name = 99;' in the source to see the compile error.");
    }
}
