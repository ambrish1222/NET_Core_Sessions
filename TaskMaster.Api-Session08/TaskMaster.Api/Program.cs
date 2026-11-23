using System.Text.Json.Serialization;
using TaskMaster.Api.Middleware;
using TaskMaster.Api.Repositories;
using TaskMaster.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Register services in the Dependency Injection (DI) container ---
builder.Services
    .AddControllers(options =>
    {
        // By default ASP.NET Core removes the "Async" suffix from action names, which would make
        // CreatedAtAction(nameof(GetByIdAsync), ...) fail at runtime ("No route matches...").
        // Turning this off lets us use the Async method names exactly as written in the slides.
        options.SuppressAsyncSuffixInActionNames = false;
    })
    .AddJsonOptions(options =>
    {
        // Enums can be sent as "High" or 3, and are shown as text in Swagger.
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controller -> ITaskService -> ITaskRepository
//
// The repository is a SINGLETON on purpose: it keeps the tasks in RAM, so one instance must live for
// the whole app. With AddScoped a new, empty repository would be created for every request and
// a task you POST would "disappear" before the next GET.
// (When we move to EF Core in Week 3 the repository becomes Scoped.)
builder.Services.AddSingleton<ITaskRepository, InMemoryTaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// --- Configure the HTTP request pipeline (middleware order matters) ---
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
