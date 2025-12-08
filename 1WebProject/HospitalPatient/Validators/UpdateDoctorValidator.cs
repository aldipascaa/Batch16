using FluentValidation;

namespace HospitalPatient.DTOs;

public class UpdateDoctorValidator : AbstractValidator<DoctorUpdateDto>
{
    public UpdateDoctorValidator()
    {
        RuleFor(x => x.FullName)
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(50);
        
        RuleFor(x => x.DepartmentId)
        .NotEmpty();
    }
}
