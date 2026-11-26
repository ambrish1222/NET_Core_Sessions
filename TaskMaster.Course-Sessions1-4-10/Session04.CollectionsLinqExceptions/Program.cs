using TaskMaster.Session04.Demos;
using TaskMaster.Session04.Exercises;
using TaskMaster.Session04.Helpers;
using TaskMaster.Session04.Homework;

// Session 4: Collections, LINQ and exceptions. Pick an item, or run one directly:
//   dotnet run -- 4      dotnet run -- E1      dotnet run -- all
var entries = new List<DemoEntry>
{
    new("1",  "List<T>",                               "4-6",   CollectionsDemo.ListBasics),
    new("2",  "Dictionary<TKey,TValue>",               "7-8",   CollectionsDemo.DictionaryBasics),
    new("3",  "IEnumerable<T>",                        "9-10",  CollectionsDemo.EnumerableBasics),
    new("4",  "LINQ: Where",                           "11-13", LinqDemo.WhereFilter),
    new("5",  "LINQ: Select",                          "14-15", LinqDemo.SelectProject),
    new("6",  "LINQ: OrderBy",                         "16",    LinqDemo.OrderBySort),
    new("7",  "LINQ: FirstOrDefault, Any, Count",      "17-19", LinqDemo.FindAnyCount),
    new("8",  "LINQ: chaining and ToList",             "20,22", LinqDemo.Chaining),
    new("9",  "Deferred execution",                    "21-22", DeferredExecutionDemo.Run),
    new("10", "Exceptions: throw and try/catch",       "23-25", ExceptionsDemo.ThrowAndCatch),
    new("11", "Guard clauses",                         "26-27", ExceptionsDemo.GuardClauses),
    new("12", "Expected vs unexpected problems",       "23",    ExceptionsDemo.ExpectedVsUnexpected),
    new("E1", "Exercise 1: Task queries",              "28",    TaskQueriesExercise.Run),
    new("E2", "Exercise 2: Task repository",           "29",    RepositoryExercise.Run),
    new("E3", "Exercise 3: Exception handling",        "30",    ExceptionHandlingExercise.Run),
    new("H",  "Homework: filtering, search, sorting",  "32",    HomeworkDemo.Run),
};

DemoMenu.Run("Session 4: Collections, LINQ & Exceptions (TaskMaster API)", entries, args);
