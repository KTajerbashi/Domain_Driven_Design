using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Courses.Commands.Create;

public sealed class CourseCreateValidator : AbstractValidator<CourseCreateCommand>
{
    public CourseCreateValidator()
    {

    }
}
