
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Services;

public class TodoService : ITodoService
{
    private readonly TodoDb _db;
    private readonly IMapper _mapper;

    public TodoService(TodoDb db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<TodoReadDTO>>> GetAllAsync()
    {
        var items = await _db.Todos.AsNoTracking()
            .ProjectTo<TodoReadDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return ServiceResult<List<TodoReadDTO>>.Ok(items);
    }

    public async Task<ServiceResult<TodoReadDTO>> GetByIdAsync(int id)
    {
        var entity = await _db.Todos.FindAsync(id);
        if (entity is null)
            return ServiceResult<TodoReadDTO>.NotFound($"Todo {id} not found.");

        return ServiceResult<TodoReadDTO>.Ok(_mapper.Map<TodoReadDTO>(entity));
    }

    public async Task<ServiceResult<TodoReadDTO>> CreateAsync(TodoCreateDTO input)
    {
        var entity = _mapper.Map<Todo>(input);
        entity.CreateAt = DateTime.UtcNow;

        _db.Todos.Add(entity);
        await _db.SaveChangesAsync();

        var dto = _mapper.Map<TodoReadDTO>(entity);
        return ServiceResult<TodoReadDTO>.Created(dto);
    }

    public async Task<ServiceResult<bool>> UpdateAsync(int id, TodoUpdateDTO input)
    {
        var entity = await _db.Todos.FindAsync(id);
        if (entity is null)
            return ServiceResult<bool>.NotFound($"Todo {id} not found.");

        var wasCompleted = entity.IsCompleted;
        entity.Title = input.Title;
        entity.IsCompleted = input.IsCompleted?? entity.IsCompleted;
        entity.UpdateAt = DateTime.UtcNow;
        entity.CompletedAt = entity.IsCompleted
            ? (wasCompleted ? entity.CompletedAt : DateTime.UtcNow)
            : null;

        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }

    public async Task<ServiceResult<bool>> DeleteAsync(int id)
    {
        var entity = await _db.Todos.FindAsync(id);
        if (entity is null)
            return ServiceResult<bool>.NotFound($"Todo {id} not found.");

        _db.Todos.Remove(entity);
        await _db.SaveChangesAsync();
        return ServiceResult<bool>.Ok(true);
    }
}
