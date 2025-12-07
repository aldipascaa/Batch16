using FluentValidation;
using ToDoList.Models;

namespace ToDoList.DTOs;

public class TodoUpdateDTOValidator : AbstractValidator<TodoUpdateDTO>
{
    public TodoUpdateDTOValidator()
    {
        RuleFor(x =>x.Title)
        .NotEmpty().WithMessage("Title is required.")
        .MaximumLength(200).WithMessage("Title max length is 200.");
    }
}
