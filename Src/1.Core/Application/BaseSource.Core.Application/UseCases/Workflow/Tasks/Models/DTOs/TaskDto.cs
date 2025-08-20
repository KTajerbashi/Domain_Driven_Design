using BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Enums;

namespace BaseSource.Core.Application.UseCases.Workflow.Tasks.Models.DTOs;

public class TaskDto : BaseDTO
{
    public Guid EntityId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TaskPriorityEnum Priority { get; set; }
    public TaskStatusEnum Status { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}