using TaskMaster.Session01.Demos;
using TaskMaster.Session01.Exercises;
using TaskMaster.Session01.Helpers;

// Session 1: C# & .NET Foundations. Pick an item from the menu, or run one directly:
//   dotnet run -- 5      dotnet run -- E3      dotnet run -- all
var entries = new List<DemoEntry>
{
    new("1",  "C# vs .NET, SDK vs Runtime",        "3-6",   PlatformDemo.LanguageVsPlatform),
    new("2",  ".NET CLI commands and project files", "7-9",  PlatformDemo.CliCommands),
    new("3",  "Your first program",                "10",    PlatformDemo.FirstProgram),
    new("4",  "Variables",                         "11",    VariablesDemo.Declaring),
    new("5",  "Common data types",                 "12",    VariablesDemo.DataTypes),
    new("6",  "String interpolation ($)",          "13",    VariablesDemo.Interpolation),
    new("7",  "DateTime and Guid",                 "14",    TaskDemo.DateTimeAndGuid),
    new("8",  "The var keyword",                   "15",    VariablesDemo.VarKeyword),
    new("9",  "Our first Task: 6 variables",       "16-18", TaskDemo.TaskVariables),
    new("E1", "Exercise 1: Student information",   "19",    StudentInfoExercise.Run),
    new("E2", "Exercise 2: Task info and reflection", "20", TaskInfoExercise.Run),
    new("E3", "Exercise 3: Arithmetic",            "21",    ArithmeticExercise.Run),
};

DemoMenu.Run("Session 1: C# & .NET Foundations (TaskMaster API)", entries, args);
