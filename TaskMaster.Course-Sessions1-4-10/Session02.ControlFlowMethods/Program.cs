using TaskMaster.Session02.Demos;
using TaskMaster.Session02.Exercises;
using TaskMaster.Session02.Helpers;
using TaskMaster.Session02.Homework;

// Session 2: Control flow, methods and nullability. Pick an item, or run one directly:
//   dotnet run -- 4      dotnet run -- E1      dotnet run -- all
var entries = new List<DemoEntry>
{
    new("1",  "Quick recap",                            "3",     DecisionsDemo.Recap),
    new("2",  "Comparison and logical operators",       "4-5",   DecisionsDemo.Operators),
    new("3",  "if / else and multiple conditions",      "6-7",   DecisionsDemo.IfElse),
    new("4",  "switch expression",                      "8",     DecisionsDemo.SwitchExpression),
    new("5",  "for loop",                               "9",     LoopsDemo.ForLoop),
    new("6",  "foreach loop",                           "10",    LoopsDemo.ForeachLoop),
    new("7",  "Methods, parameters, return values",     "11-15", MethodsDemo.Run),
    new("8",  "null and string?",                       "16-17", NullDemo.NullableReference),
    new("9",  "The ?? and ?. operators",                "18-19", NullDemo.NullOperators),
    new("10", "Empty vs null",                          "20",    NullDemo.EmptyVsNull),
    new("11", "Task title validation",                  "21",    NullDemo.TitleValidation),
    new("E1", "Exercise 1: Priority label (switch)",    "22",    PriorityLabelExercise.Run),
    new("E2", "Exercise 2: Task status",                "23",    TaskStatusExercise.Run),
    new("E3", "Exercise 3: Process tasks",              "24",    ProcessTasksExercise.Run),
    new("E4", "Exercise 4: Title validation",           "25",    ValidationExercise.Run),
    new("H",  "Homework: TaskValidator in action",      "27",    HomeworkDemo.Run),
};

DemoMenu.Run("Session 2: Control Flow, Methods & Nullability (TaskMaster API)", entries, args);
