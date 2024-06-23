using CourseProject.Common.Dtos.Employee;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class EmployeeCreateValidatior : AbstractValidator<EmployeeCreate>
{
    public EmployeeCreateValidatior()
    {
        RuleFor(employeeCreate => employeeCreate.FirstName).NotEmpty().MaximumLength(50);
        RuleFor(employeeCreate => employeeCreate.LastName).NotEmpty().MaximumLength(50);
    }
}
