using System.Runtime.InteropServices;

namespace TaskMaster.Session01.Demos;

/// <summary>Slides 3-10: C# vs .NET, SDK vs Runtime, the CLI, and the first program.</summary>
public static class PlatformDemo
{
    // Slides 3-6
    public static void LanguageVsPlatform()
    {
        Console.WriteLine("C#   = the LANGUAGE you type (syntax, keywords: int, string, if, class).");
        Console.WriteLine(".NET = the PLATFORM that builds and runs it (SDK, runtime, libraries, CLI).");
        Console.WriteLine();
        Console.WriteLine("SDK     = Develop (compiler + CLI + MSBuild; it also contains the runtime).");
        Console.WriteLine("Runtime = Run     (CLR + JIT + garbage collector; what production servers need).");
        Console.WriteLine();
        Console.WriteLine("This program is running right now on:");
        Console.WriteLine($"  .NET runtime : {RuntimeInformation.FrameworkDescription}");
        Console.WriteLine($"  Version      : {Environment.Version}");
        Console.WriteLine($"  OS           : {RuntimeInformation.OSDescription}");
        Console.WriteLine($"  Architecture : {RuntimeInformation.OSArchitecture}");
    }

    // Slides 7-9
    public static void CliCommands()
    {
        Console.WriteLine("Run these in PowerShell, Terminal or Command Prompt:");
        Console.WriteLine();
        Console.WriteLine("  dotnet --version               check the SDK version");
        Console.WriteLine("  dotnet --list-sdks             installed SDKs");
        Console.WriteLine("  dotnet --list-runtimes         installed runtimes");
        Console.WriteLine("  dotnet new console -n TaskApi  create a new console project");
        Console.WriteLine("  dotnet build                   compile (finds errors)");
        Console.WriteLine("  dotnet run                     compile and run");
        Console.WriteLine();
        Console.WriteLine("Project files (slide 9):");
        Console.WriteLine("  Program.cs  -> where execution starts (top-level statements)");
        Console.WriteLine("  *.csproj    -> XML settings: OutputType, TargetFramework, Nullable, ImplicitUsings");
    }

    // Slide 10
    public static void FirstProgram()
    {
        Console.WriteLine("Welcome to C#");
        Console.WriteLine("Welcome to Task API");
        // Console = class, WriteLine = method, "..." = string, ; ends every statement.
    }
}
