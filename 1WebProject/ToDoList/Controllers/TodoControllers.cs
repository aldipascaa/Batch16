using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;
using ToDoList.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using ToDoList.Services;

namespace ToDoList.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;
    public TodoController(ITodoService service)
    =>_service = service;

    // GET: api/all
    [HttpGet]
    public async Task<IActionResult> GetAll()
    =>this.ToActionResult(await _service.GetAllAsync());

    // GET: api/Todos/{}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    => this.ToActionResult(await _service.GetByIdAsync(id));


    // POST: api/Todos/{}
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDTO input)
    =>this.ToActionResult(await _service.CreateAsync(input));

    // PUT: api/Todos/{}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDTO input)
    => this.ToActionResult(await _service.UpdateAsync(id, input));


    // DELETE: api/Todos/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    => this.ToActionResult(await _service.DeleteAsync(id));
}