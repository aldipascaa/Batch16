
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalPatient.Data;
using HospitalPatient.DTOs;
using HospitalPatient.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatient.Services;

public class PatientService : IPatientService
{
    private readonly HospitalDb _db;
    private readonly IMapper _mapper;

    public PatientService(HospitalDb db, IMapper mapper)
    {
        _db = db; _mapper = mapper;
    }

    public async Task<ServiceResult<List<PatientReadDto>>> GetAllAsync()
    {
        var list = await _db.Patients.AsNoTracking()
            .ProjectTo<PatientReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return ServiceResult<List<PatientReadDto>>.Ok(list);
    }

    public async Task<ServiceResult<PatientReadDto>> GetByIdAsync(int id)
    {
        var e = await _db.Patients.FindAsync(id);
        if (e is null) return ServiceResult<PatientReadDto>.NotFound("Patient not found");
        return ServiceResult<PatientReadDto>.Ok(_mapper.Map<PatientReadDto>(e));
    }

    public async Task<ServiceResult<PatientReadDto>> CreateAsync(PatientCreateDto dto)
    {
        if (await _db.Patients.AnyAsync(p => p.MedicalRecordNumber == dto.MedicalRecordNumber))
            return ServiceResult<PatientReadDto>.BadRequest("MRN already exists");

        var e = _mapper.Map<Patient>(dto);
        _db.Patients.Add(e);
        await _db.SaveChangesAsync();
        return ServiceResult<PatientReadDto>.Created(_mapper.Map<PatientReadDto>(e));
    }

    public async Task<ServiceResult<bool>> UpdateAsync(int id, PatientUpdateDto dto)
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
