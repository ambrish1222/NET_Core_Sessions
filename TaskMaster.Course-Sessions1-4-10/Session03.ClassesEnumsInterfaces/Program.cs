using TaskMaster.Session03.Demos;
using TaskMaster.Session03.Exercises;
using TaskMaster.Session03.Helpers;
using TaskMaster.Session03.Homework;

// Session 3: Classes, objects, enums and interfaces. Pick an item, or run one directly:
//   dotnet run -- 2      dotnet run -- E2      dotnet run -- all
var entries = new List<DemoEntry>
{
    new("1",  "Recap: a method from Session 2",         "3",     ClassesDemo.Recap),
    new("2",  "Classes, objects, initializers",         "4-7,12", ClassesDemo.ClassAndObject),
    new("3",  "Properties",                             "8-9",   ClassesDemo.Properties),
    new("4",  "Constructors",                           "10-11", ClassesDemo.Constructors),
    new("5",  "Enums",                                  "13-17", EnumsDemo.Run),
    new("6",  "Interfaces and implementations",         "18-20,23-24", InterfaceDemo.Run),
    new("7",  "The Task API model",                     "21-22", TaskModelDemo.Run),
    new("E1", "Exercise 1: Build TaskItem",             "25",    BuildTaskItemExercise.Run),
    new("E2", "Exercise 2: Repository",                 "26",    RepositoryExercise.Run),
    new("E3", "Exercise 3: Explain the design",         "27",    ExplainDesignExercise.Run),
    new("H",  "Homework checklist",                     "29",    HomeworkDemo.Run),
};

DemoMenu.Run("Session 3: Classes, Objects, Enums & Interfaces (TaskMaster API)", entries, args);
