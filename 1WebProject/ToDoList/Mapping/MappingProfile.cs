using AutoMapper;
using ToDoList.DTOs;
using ToDoList.Models;

namespace ToDoList.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoReadDTO>();

        CreateMap<TodoCreateDTO, Todo>()
        .ForMember(t => t.IsCompleted, opt=> opt.MapFrom(_ => false))
        .ForMember(t => t.Priority, opt=> opt.MapFrom(_ => 0))
        .ForMember(t => t.CreateAt, opt=> opt.MapFrom(_ => DateTime.Now))
        .ForMember(t => t.UpdateAt, opt=> opt.Ignore())
        .ForMember(t => t.CompletedAt, opt=> opt.Ignore());

        CreateMap<TodoUpdateDTO, Todo>()
        .ForMember(t => t.Id, opt=> opt.Ignore())
        .ForMember(t => t.Priority, opt=> opt.Ignore())
        .ForMember(t => t.CreateAt, opt=> opt.Ignore())
        .ForMember(t => t.UpdateAt, opt=> opt.MapFrom(_ => DateTime.Now))
        .ForMember(t => t.CompletedAt, opt=> opt.Ignore());





    }
}
