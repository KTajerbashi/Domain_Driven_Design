using BaseSource.Core.Application.UseCases.Workflow.Tasks.Commands.CompleteTask;
using BaseSource.Core.Application.UseCases.Workflow.Tasks.Commands.Create;
using BaseSource.Core.Application.UseCases.Workflow.Tasks.Commands.Update;
using BaseSource.Core.Application.UseCases.Workflow.Tasks.Queries.GetAllTasks;
using BaseSource.Core.Application.UseCases.Workflow.Tasks.Queries.GetTaskById;
using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Workflow;

public class TasksController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await Factory.Mediator.Send(new GetAllTasksQuery());

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await Factory.Mediator.Send(new GetTaskByIdQuery(id));

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
    {
        var result = await Factory.Mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskCommand command)
    {
        if (id != command.Id)
            return BadRequest("ID mismatch");

        var result = await Factory.Mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> Complete(Guid id)
    {
        var result = await Factory.Mediator.Send(new CompleteTaskCommand(id));

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}
