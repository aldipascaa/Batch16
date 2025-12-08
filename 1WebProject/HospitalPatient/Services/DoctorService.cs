
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalPatient.Data;
using HospitalPatient.DTOs;
using HospitalPatient.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatient.Services;

public class DoctorService : IDoctorService
{
    private readonly HospitalDb _db;
    private readonly IMapper _mapper;

    public DoctorService(HospitalDb db, IMapper mapper)
    {
        _db = db; _mapper = mapper;
    }

    public async Task<ServiceResult<List<DoctorReadDto>>> GetAllAsync()
    {
        var list = await _db.Doctors.AsNoTracking()
            .ProjectTo<DoctorReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return ServiceResult<List<DoctorReadDto>>.Ok(list);
    }

    public async Task<ServiceResult<DoctorReadDto>> GetByIdAsync(int id)
    {
        var e = await _db.Patients.FindAsync(id);
        if (e is null) return ServiceResult<DoctorReadDto>.NotFound("Patient not found");
        return ServiceResult<DoctorReadDto>.Ok(_mapper.Map<DoctorReadDto>(e));
    }

    public async Task<ServiceResult<DoctorReadDto>> CreateAsync(DoctorCreateDto dto)
    {
        if (await _db.Doctors.AnyAsync(p => p.LicenseNumber == dto.LicenseNumber))
            return ServiceResult<DoctorReadDto>.BadRequest("MRN already exists");

        var e = _mapper.Map<Doctor>(dto);
        _db.Doctors.Add(e);
        await _db.SaveChangesAsync();
        return ServiceResult<DoctorReadDto>.Created(_mapper.Map<DoctorReadDto>(e));
    }

    public async Task<ServiceResult<bool>> UpdateAsync(int id, DoctorUpdateDto dto)
    {
        var e = await _db.Patients.FindAsync(id);
        if (e is null) return ServiceResult<bool>.NotFound("Patient not found");
        _mapper.Map(dto, e);
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var e = await _db.Patients.FindAsync(id);
        if (e is null) return ServiceResult<bool>.NotFound("Patient not found");
        _db.Patients.Remove(e);
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }
}
