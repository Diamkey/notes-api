using Application.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Notes.Commands;

public sealed record AddNoteRequest(string Content)
    : IRequest;
    
public sealed class AddNoteRequestHandler(INoteRepository repository, IMapper mapper)
    : IRequestHandler<AddNoteRequest>
{
    public async Task Handle(AddNoteRequest request, CancellationToken cancellationToken)
    {
        var note = Note.Create(request.Content);
        await repository.AddNoteAsync(note);
    }
}