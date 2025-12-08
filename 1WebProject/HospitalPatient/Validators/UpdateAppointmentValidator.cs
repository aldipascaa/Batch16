
using FluentValidation;

namespace HospitalPatient.DTOs;

public class AppointmentUpdateDtoValidator : AbstractValidator<AppointmentUpdateDto>
{
    public AppointmentUpdateDtoValidator()
    {
        RuleFor(x => x.ScheduledAt).GreaterThan(DateTime.UtcNow.AddDays(7)); // in the next 7 day
        RuleFor(x => x.DurationMinutes).InclusiveBetween(15, 180);
        RuleFor(x => x.Notes).MaximumLength(1000).When(x => !string.IsNullOrWhiteSpace(x.Notes));
    }
}
