using FluentValidation;

namespace ToDoList.DTOs;

public class TodoCreateDTOValidator : AbstractValidator<TodoCreateDTO>
{
    public TodoCreateDTOValidator()
    {
        RuleFor(x =>x.Title)
        .NotEmpty().WithMessage("Title is required.")
        .MaximumLength(200).WithMessage("Title max length is 200.");
    }
}