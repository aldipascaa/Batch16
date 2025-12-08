using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalPatient.Data;
using HospitalPatient.DTOs;
using HospitalPatient.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatient.Services;

public class AppointmentService : IAppointmentService
{
    private readonly HospitalDb _db;
    private readonly IMapper _mapper;

    public AppointmentService(HospitalDb db, IMapper mapper)
    {
        _db = db; _mapper = mapper;
    }

    public async Task<ServiceResult<List<AppointmentReadDto>>> GetAllAsync()
    {
        var items = await _db.Appointments.AsNoTracking()
            .Include(a => a.Patient).Include(a => a.Doctor)
            .ProjectTo<AppointmentReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return ServiceResult<List<AppointmentReadDto>>.Ok(items);
    }

    public async Task<ServiceResult<AppointmentReadDto>> GetByIdAsync(int id)
    {
        var a = await _db.Appointments
            .Include(x => x.Patient).Include(x => x.Doctor)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (a is null) return ServiceResult<AppointmentReadDto>.NotFound("Appointment not found");
        return ServiceResult<AppointmentReadDto>.Ok(_mapper.Map<AppointmentReadDto>(a));
    }

    public async Task<ServiceResult<AppointmentReadDto>> CreateAsync(AppointmentCreateDto dto)
    {
        // Basic FK existence checks
        if (!await _db.Patients.AnyAsync(p => p.Id == dto.PatientId))
            return ServiceResult<AppointmentReadDto>.BadRequest("Patient does not exist");
        if (!await _db.Doctors.AnyAsync(d => d.Id == dto.DoctorId))
            return ServiceResult<AppointmentReadDto>.BadRequest("Doctor does not exist");

        // Overlap check for the same doctor
        var start = dto.ScheduledAt;
        var end   = dto.ScheduledAt.AddMinutes(dto.DurationMinutes);

        var overlap = await _db.Appointments.AnyAsync(a =>
            a.DoctorId == dto.DoctorId &&
            a.ScheduledAt < end &&
            a.ScheduledAt.AddMinutes(a.DurationMinutes) > start);

        if (overlap)
            return ServiceResult<AppointmentReadDto>.BadRequest("Appointment overlaps with another for this doctor");

        var e = _mapper.Map<Appointment>(dto);
        e.CreatedAt = DateTime.UtcNow;
        _db.Appointments.Add(e);
        await _db.SaveChangesAsync();

        var withRefs = await _db.Appointments.Include(x => x.Patient).Include(x => x.Doctor).FirstAsync(x => x.Id == e.Id);
        return ServiceResult<AppointmentReadDto>.Created(_mapper.Map<AppointmentReadDto>(withRefs));
    }

    public async Task<ServiceResult<bool>> UpdateAsync(int id, AppointmentUpdateDto dto)
    {
        var e = await _db.Appointments.FindAsync(id);
        if (e is null) return ServiceResult<bool>.NotFound("Appointment not found");

        // Overlap check again
        var newStart = dto.ScheduledAt;
        var newEnd   = dto.ScheduledAt.AddMinutes(dto.DurationMinutes);

        var overlap = await _db.Appointments.AnyAsync(a =>
            a.DoctorId == e.DoctorId && a.Id != e.Id &&
            a.ScheduledAt < newEnd &&
            a.ScheduledAt.AddMinutes(a.DurationMinutes) > newStart);

        if (overlap)
            return ServiceResult<bool>.BadRequest("Appointment overlaps with another for this doctor");

        _mapper.Map(dto, e);
        e.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var e = await _db.Appointments.FindAsync(id);
        if (e is null) return ServiceResult<bool>.NotFound("Appointment not found");
        _db.Appointments.Remove(e);
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }
}
