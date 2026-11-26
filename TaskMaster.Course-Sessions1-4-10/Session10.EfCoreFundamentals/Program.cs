using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Api.Data;
using TaskMaster.Api.Middleware;
using TaskMaster.Api.Repositories;
using TaskMaster.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Register services in the Dependency Injection (DI) container ---
builder.Services
    .AddControllers(options =>
    {
        // Lets CreatedAtAction(nameof(GetByIdAsync), ...) work with the Async method names used in the slides.
        options.SuppressAsyncSuffixInActionNames = false;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller -> ITaskService -> ITaskRepository -> TaskDbContext -> SQL Server   (slide 22)
//
// "Persistence:Provider" in appsettings.json picks the repository (Exercise 3):
//   SqlServer (default) : EF Core repository, data is kept in the database
//   InMemory            : the Week 2 list in RAM, needs no database at all
var provider = builder.Configuration["Persistence:Provider"] ?? "SqlServer";
var useSqlServer = !string.Equals(provider, "InMemory", StringComparison.OrdinalIgnoreCase);

if (useSqlServer)
{
    // Slides 10-12. Fail early with a clear message instead of a confusing error on the first request
    // (common mistake #1, slide 25: missing or incorrect connection string).
    var connectionString = builder.Configuration.GetConnectionString("TaskDb")
        ?? throw new InvalidOperationException(
            "Connection string 'TaskDb' was not found. Check ConnectionStrings:TaskDb in appsettings.json, " +
            "or set the environment variable ConnectionStrings__TaskDb.");

    // Slide 10. Registers TaskDbContext as SCOPED (one per request).
    builder.Services.AddDbContext<TaskDbContext>(options =>
        options.UseSqlServer(connectionString));

    // Slide 24. Scoped is REQUIRED here: the repository holds a scoped DbContext, so it cannot be a singleton.
    // (The in-memory repository was a singleton for the opposite reason: it owned the data.)
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
}
else
{
    builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
}

builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// --- Configure the HTTP request pipeline (middleware order matters) ---
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

if (useSqlServer)
{
    // Exercise 1 check: "Can the app reach SQL Server?"  GET /health/db
    app.MapGet("/health/db", async (TaskDbContext db, CancellationToken cancellationToken) =>
    {
        var canConnect = await db.Database.CanConnectAsync(cancellationToken);
        return canConnect
            ? Results.Ok(new { database = "reachable" })
            : Results.Problem("Cannot connect to the database. Check the connection string and that SQL Server is running.");
    });
}

app.Run();
