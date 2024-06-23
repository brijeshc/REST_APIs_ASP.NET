using CourseProject.Common.Dtos.Teams;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class TeamCreateValidatior : AbstractValidator<TeamCreate>
{
    public TeamCreateValidatior()
    {
        RuleFor(TeamCreate => TeamCreate.Name).NotEmpty().MaximumLength(50);
    }
}
