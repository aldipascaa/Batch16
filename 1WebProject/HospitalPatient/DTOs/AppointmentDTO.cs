namespace HospitalPatient.DTOs;

public record AppointmentReadDto(
    int Id, int PatientId, string PatientName,
    int DoctorId, string DoctorName,
    DateTime ScheduledAt, int DurationMinutes, string? Notes);

public record AppointmentCreateDto(
    int PatientId, int DoctorId, DateTime ScheduledAt, int DurationMinutes, string? Notes);

public record AppointmentUpdateDto(
    DateTime ScheduledAt, int DurationMinutes, string? Notes);
