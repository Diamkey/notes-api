using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Notes.Commands;

public record AddNoteRequest(string Content)
    : IRequest;
    
internal sealed class AddNoteRequestHandler(INoteRepository repository, IMapper mapper)
    : IRequestHandler<AddNoteRequest>
{
    public async Task Handle(AddNoteRequest request, CancellationToken cancellationToken)
    {
        var note = Note.Create(request.Content);
        await repository.AddNoteAsync(note);
    }
}