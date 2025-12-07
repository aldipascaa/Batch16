
using ToDoList.DTOs;

namespace ToDoList.Services;

public interface ITodoService
{
    Task<ServiceResult<List<TodoReadDTO>>> GetAllAsync();
    Task<ServiceResult<TodoReadDTO>> GetByIdAsync(int id);
    Task<ServiceResult<TodoReadDTO>> CreateAsync(TodoCreateDTO input);
    Task<ServiceResult<bool>> UpdateAsync(int id, TodoUpdateDTO input);
    Task<ServiceResult<bool>> DeleteAsync(int id);
}
