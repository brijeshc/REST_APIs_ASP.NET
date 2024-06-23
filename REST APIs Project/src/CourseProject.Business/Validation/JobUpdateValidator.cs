using CourseProject.Common.Dtos.Job;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class JobUpdateValidatior : AbstractValidator<JobUpdate>
{
    public JobUpdateValidatior()
    {
        RuleFor(JobUpdate => JobUpdate.Name).NotEmpty().MaximumLength(50);
        RuleFor(JobUpdate => JobUpdate.Description).NotEmpty().MaximumLength(250);
    }
}
