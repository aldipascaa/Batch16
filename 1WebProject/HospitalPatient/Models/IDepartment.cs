
namespace HospitalPatient.Models;

public class IDepartment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty; // unique

    public ICollection<IDoctor> Doctors { get; set; } = new List<IDoctor>();
}
