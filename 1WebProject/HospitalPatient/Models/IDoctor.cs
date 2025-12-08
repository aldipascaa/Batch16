
namespace HospitalPatient.Models;

public class IDoctor
{
    public int Id { get; set; }
    public string LicenseNumber { get; set; } = string.Empty; // unique
    public string FullName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public IDepartment? Department { get; set; }

    public ICollection<IAppointment> Appointments { get; set; } = new List<IAppointment>();
}
