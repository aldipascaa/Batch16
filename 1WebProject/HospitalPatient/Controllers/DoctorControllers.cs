
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HospitalPatient.DTOs;
using HospitalPatient.Services;

namespace HospitalPatient.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // require JWT
public class DoctorsController : ControllerBase
{
    private readonly IDoctorService _svc;
    public DoctorsController(IDoctorService svc) => _svc = svc;

    [HttpGet]
    [Authorize(Roles = "Admin,Staff,Doctor")]
    public async Task<IActionResult> GetAll() => this.ToActionResult(await _svc.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Staff,Doctor")]
    public async Task<IActionResult> Get(int id) => this.ToActionResult(await _svc.GetByIdAsync(id));

    [HttpPost]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Create([FromBody] DoctorCreateDto dto) => this.ToActionResult(await _svc.CreateAsync(dto));

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Update(int id, [FromBody] DoctorUpdateDto dto) => this.ToActionResult(await _svc.UpdateAsync(id, dto));

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id) => this.ToActionResult(await _svc.DeleteAsync(id));
}
