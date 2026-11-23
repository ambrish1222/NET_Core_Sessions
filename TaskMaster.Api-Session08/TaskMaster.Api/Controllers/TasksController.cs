using Microsoft.AspNetCore.Mvc;
using TaskMaster.Api.DTOs;
using TaskMaster.Api.Services;

namespace TaskMaster.Api.Controllers;

/// <summary>
/// Thin controller: translate HTTP into a service call, and the result back into HTTP.
/// No SQL, no business rules, no mapping.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _service;

    // ASP.NET Core's DI container supplies the service. We never write "new TaskService()".
    public TasksController(ITaskService service)
    {
        _service = service;
    }

    // GET /api/tasks
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TaskResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    // GET /api/tasks/10
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(
            id, cancellationToken);

        if (result is null)
            return NotFound();

        return Ok(result);
    }

    // POST /api/tasks
    [HttpPost]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(
        CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.CreateAsync(
            request, cancellationToken);

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id = result.Id },
            result);
    }

    // PUT /api/tasks/10
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(
        int id,
        UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, request, cancellationToken);

        return updated ? NoContent() : NotFound();
    }

    // DELETE /api/tasks/10
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _service.DeleteAsync(id, cancellationToken);

        return deleted ? NoContent() : NotFound();
    }
}
