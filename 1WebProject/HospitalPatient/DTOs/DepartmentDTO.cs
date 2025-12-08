namespace HospitalPatient.DTOs;

public record DepartmentReadDto(int Id, string Name);
public record DepartmentCreateDto(string Name);
public record DepartmentUpdateDto(string Name);
