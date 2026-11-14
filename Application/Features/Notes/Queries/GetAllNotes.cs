using Application.DTOs;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Notes.Queries;

public record GetAllNotesRequest 
    : IRequest<IReadOnlyList<NoteDto>>;
    
internal sealed class GetAllNotesRequestHandler(INoteRepository repository, IMapper mapper)
    :  IRequestHandler<GetAllNotesRequest, IReadOnlyList<NoteDto>>
{
    public async Task<IReadOnlyList<NoteDto>> Handle(GetAllNotesRequest request, 
        CancellationToken cancellationToken)
    {
        var notes = await repository.GetAllNotesAsync();
        return mapper.Map<IReadOnlyList<NoteDto>>(notes);
    }
}