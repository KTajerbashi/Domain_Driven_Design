namespace MiniBlog.Core.Domain.Advertisements.Parameters.Courses;

public record CourseDeleteParameter(
    string Name,
    int Length,
    DateTime From,
    DateTime To);

