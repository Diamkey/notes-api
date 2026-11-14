using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Note, NoteDto>()
            .ForCtorParam(nameof(NoteDto.CreatedAt), opt => 
                opt.MapFrom(src => 
                    src.CreatedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm")));
    }
}