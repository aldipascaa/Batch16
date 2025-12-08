
using HospitalPatient.DTOs;

namespace HospitalPatient.Services;

public interface IPatientService
{
    Task<ServiceResult<List<PatientReadDto>>> GetAllAsync();
    Task<ServiceResult<PatientReadDto>> GetByIdAsync(int id);
    Task<ServiceResult<PatientReadDto>> CreateAsync(PatientCreateDto dto);
    Task<ServiceResult<bool>> UpdateAsync(int id, PatientUpdateDto dto);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}
