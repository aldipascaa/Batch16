
using System.Data;
using FluentValidation;

namespace HospitalPatient.DTOs;

public class UpdatePatientValidator : AbstractValidator<PatientUpdateDto>
{
    public UpdatePatientValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow);
        RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email));
        RuleFor(x => x.Phone).MaximumLength(20).When(x => !string.IsNullOrWhiteSpace(x.Phone));
    }
}
