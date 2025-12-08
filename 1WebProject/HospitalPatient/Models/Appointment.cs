
namespace HospitalPatient.Models;

public class Appointment : IAppointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public IPatient? Patient { get; set; }
    public int DoctorId { get; set; }
    public IDoctor? Doctor { get; set; }

    public DateTime ScheduledAt { get; set; }  // start time
    public int DurationMinutes { get; set; }   // e.g., 30
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
