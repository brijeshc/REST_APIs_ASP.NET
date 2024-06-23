using CourseProject.Common.Dtos.Teams;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class TeamUpdateValidatior : AbstractValidator<TeamUpdate>
{
    public TeamUpdateValidatior()
    {
        RuleFor(TeamUpdate => TeamUpdate.Name).NotEmpty().MaximumLength(50);
    }
}
