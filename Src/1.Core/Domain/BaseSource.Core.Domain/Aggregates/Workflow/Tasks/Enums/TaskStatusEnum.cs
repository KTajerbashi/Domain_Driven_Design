namespace BaseSource.Core.Domain.Aggregates.Workflow.Tasks.Enums;

public enum TaskStatusEnum : byte
{
    Pending = 1,
    InProgress = 2,
    Completed = 3
}

public enum TaskPriorityEnum : byte
{
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}