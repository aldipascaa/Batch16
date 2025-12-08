namespace HospitalPatient.Models;

public class IPatient
{
    public int Id { get; set; }
    public string MedicalRecordNumber { get; set; } = string.Empty; // unique
    public string FullName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public ICollection<IAppointment> Appointments { get; set; } = new List<IAppointment>();
}
