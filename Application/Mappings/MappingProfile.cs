using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Note, NoteDto>()
            .ForMember(noteDto => noteDto.CreatedAt, 
                opt => 
                    opt.MapFrom(note => 
                        note.CreatedAt.LocalDateTime.ToString("yyyy-MM-dd HH:mm")));
    }
}