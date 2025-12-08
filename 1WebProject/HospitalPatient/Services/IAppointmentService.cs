
using HospitalPatient.DTOs;

namespace HospitalPatient.Services;

public interface IAppointmentService
{
    Task<ServiceResult<List<AppointmentReadDto>>> GetAllAsync();
    Task<ServiceResult<AppointmentReadDto>> GetByIdAsync(int id);
    Task<ServiceResult<AppointmentReadDto>> CreateAsync(AppointmentCreateDto dto);
    Task<ServiceResult<bool>> UpdateAsync(int id, AppointmentUpdateDto dto);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}
