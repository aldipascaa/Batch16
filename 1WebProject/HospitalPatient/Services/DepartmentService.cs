
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalPatient.Data;
using HospitalPatient.DTOs;
using HospitalPatient.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalPatient.Services;

public class DepartmentService : IDepartmentService
{
    private readonly HospitalDb _db;
    private readonly IMapper _mapper;

    public DepartmentService(HospitalDb db, IMapper mapper)
    {
        _db = db; _mapper = mapper;
    }

    public async Task<ServiceResult<List<DepartmentReadDto>>> GetAllAsync()
    {
        var list = await _db.Departments.AsNoTracking()
            .ProjectTo<DepartmentReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return ServiceResult<List<DepartmentReadDto>>.Ok(list);
    }

    public async Task<ServiceResult<DepartmentReadDto>> GetByIdAsync(int id)
    {
        var e = await _db.Departments.FindAsync(id);
        if (e is null) return ServiceResult<DepartmentReadDto>.NotFound("Patient not found");
        return ServiceResult<DepartmentReadDto>.Ok(_mapper.Map<DepartmentReadDto>(e));
    }

    public async Task<ServiceResult<DepartmentReadDto>> CreateAsync(DepartmentCreateDto dto)
    {
        if (await _db.Departments.AnyAsync(p => p.Name == dto.Name))
            return ServiceResult<DepartmentReadDto>.BadRequest("Department already exists");

        var e = _mapper.Map<IDepartment>(dto);
        _db.Departments.Add(e);
        await _db.SaveChangesAsync();
        return ServiceResult<DepartmentReadDto>.Created(_mapper.Map<DepartmentReadDto>(e));
    }

    public async Task<ServiceResult<bool>> UpdateAsync(int id, DepartmentUpdateDto dto)
    {
        var e = await _db.Departments.FindAsync(id);
        if (e is null) return ServiceResult<bool>.NotFound("Department not found");
        _mapper.Map(dto, e);
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var e = await _db.Departments.FindAsync(id);
        if (e is null) return ServiceResult<bool>.NotFound("Department not found");
        _db.Departments.Remove(e);
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }
}
