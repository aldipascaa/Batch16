namespace ToDoList.DTOs;

public record TodoUpdateDTO(string? Title, bool? IsCompleted);