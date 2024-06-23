using CourseProject.Common.Dtos.Employee;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class EmployeeUpdateValidatior : AbstractValidator<EmployeeUpdate>
{
    public EmployeeUpdateValidatior()
    {
        RuleFor(EmployeeUpdate => EmployeeUpdate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(EmployeeUpdate => EmployeeUpdate.LastName).NotEmpty().MaximumLength(50);
    }
}
