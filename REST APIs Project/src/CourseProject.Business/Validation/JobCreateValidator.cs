using CourseProject.Common.Dtos.Job;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class JobCreateValidatior : AbstractValidator<JobCreate>
{
    public JobCreateValidatior()
    {
        RuleFor(jobCreate => jobCreate.Name).NotEmpty().MaximumLength(50);
        RuleFor(jobCreate => jobCreate.Description).NotEmpty().MaximumLength(250);
    }
}
