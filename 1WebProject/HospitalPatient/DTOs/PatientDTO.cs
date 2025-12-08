namespace HospitalPatient.DTOs;

public record PatientReadDto(int Id, string MedicalRecordNumber, string FullName, DateTime DateOfBirth, string? Phone, string? Email);
public record PatientCreateDto(string MedicalRecordNumber, string FullName, DateTime DateOfBirth, string? Phone, string? Email);
public record PatientUpdateDto(string FullName, DateTime DateOfBirth, string? Phone, string? Email);
