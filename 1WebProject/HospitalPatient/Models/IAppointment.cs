
namespace HospitalPatient.Models;

public class IAppointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public DateTime ScheduledAt { get; set; }  // start time
    public int DurationMinutes { get; set; }   // e.g., 30
    public string? Notes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
