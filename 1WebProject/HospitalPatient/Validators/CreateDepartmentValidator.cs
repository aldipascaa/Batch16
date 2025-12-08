
using FluentValidation;

namespace HospitalPatient.DTOs;

public class CreateDepartmentValidator : AbstractValidator<DepartmentCreateDto>
{
    public CreateDepartmentValidator()
    {
        RuleFor(x => x.Name)
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(50);
    }
}
