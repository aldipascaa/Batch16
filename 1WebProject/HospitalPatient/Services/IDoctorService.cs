
using HospitalPatient.DTOs;

namespace HospitalPatient.Services;

public interface IDoctorService
{
    Task<ServiceResult<List<DoctorReadDto>>> GetAllAsync();
    Task<ServiceResult<DoctorReadDto>> GetByIdAsync(int id);
    Task<ServiceResult<DoctorReadDto>> CreateAsync(DoctorCreateDto dto);
    Task<ServiceResult<bool>> UpdateAsync(int id, DoctorUpdateDto dto);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}