using AutoMapper;
using Task.DTOs;
using Task.Models;

namespace Task.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Entity -> DTO
        CreateMap<TaskItem, TaskItemDto>();

        // Create request -> Entity
        CreateMap<CreateTaskRequest, TaskItem>()
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(src => DateTime.UtcNow)
            );

        // Update request -> Entity
        CreateMap<UpdateTaskRequest, TaskItem>();
    }
}