
using AutoMapper;
using HospitalPatient.DTOs;
using HospitalPatient.Models;

namespace HospitalPatient.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Patient
        CreateMap<IPatient, PatientReadDto>();
        CreateMap<PatientCreateDto, IPatient>();
        CreateMap<PatientUpdateDto, IPatient>();

        // Department
        CreateMap<IDepartment, DepartmentReadDto>();
        CreateMap<DepartmentCreateDto, IDepartment>();

        // Doctor
        CreateMap<IDoctor, DoctorReadDto>()
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : string.Empty));
        CreateMap<DoctorCreateDto, IDoctor>();
        CreateMap<DoctorUpdateDto, IDoctor>();

        // Appointment
        CreateMap<IAppointment, AppointmentReadDto>()
            .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Patient!.FullName))
            .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Doctor!.FullName));
        CreateMap<AppointmentCreateDto, IAppointment>()
            .ForMember(a => a.CreatedAt, o => o.MapFrom(_ => DateTime.UtcNow));
        CreateMap<AppointmentUpdateDto, IAppointment>()
            .ForMember(a => a.UpdatedAt, o => o.MapFrom(_ => DateTime.UtcNow));
    }
}
