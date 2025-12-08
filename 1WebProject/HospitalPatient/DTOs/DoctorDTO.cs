
namespace HospitalPatient.DTOs;

public record DoctorReadDto(int Id, string LicenseNumber, string FullName, int DepartmentId, string DepartmentName);
public record DoctorCreateDto(string LicenseNumber, string FullName, int DepartmentId);
public record DoctorUpdateDto(string FullName, int DepartmentId);
