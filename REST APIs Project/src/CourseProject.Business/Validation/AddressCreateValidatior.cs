using CourseProject.Common.Dtos.Address;
using FluentValidation;

namespace CourseProject.Business.Validation;

public class AddressCreateValidatior : AbstractValidator<AddressCreate>
{
    public AddressCreateValidatior()
    {
        RuleFor(addressCreate => addressCreate.Email).NotEmpty().EmailAddress().MaximumLength(100);
        RuleFor(addressCreate => addressCreate.City).NotEmpty().MaximumLength(100);
        RuleFor(addressCreate => addressCreate.Street).NotEmpty().MaximumLength(100);
        RuleFor(addressCreate => addressCreate.Zip).NotEmpty().MaximumLength(16);
        RuleFor(addressCreate => addressCreate.Phone).NotEmpty().MaximumLength(32);
    }
}
