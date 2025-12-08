
using HospitalPatient.DTOs;

namespace HospitalPatient.Services;

public interface IDepartmentService
{
    Task<ServiceResult<List<DepartmentReadDto>>> GetAllAsync();
    Task<ServiceResult<DepartmentReadDto>> GetByIdAsync(int id);
    Task<ServiceResult<DepartmentReadDto>> CreateAsync(DepartmentCreateDto dto);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}
