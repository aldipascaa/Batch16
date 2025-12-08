
using System.Data;
using FluentValidation;

namespace HospitalPatient.DTOs;

public class CreateDoctorValidator : AbstractValidator<DoctorCreateDto>
{
    public CreateDoctorValidator()
    {
        RuleFor(x => x.FullName)
        .NotEmpty()
        .MinimumLength(5)
        .MaximumLength(50);
        
        RuleFor(x => x.LicenseNumber)
        .NotEmpty();
        
        RuleFor(x => x.DepartmentId)
        .NotEmpty();
    }
}
