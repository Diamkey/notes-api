using Application.Exceptions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Notes.Commands;

public record DeleteNoteRequest(Guid Id)
    : IRequest;
    
internal sealed class DeleteNoteRequestHandler(INoteRepository repository)
    : IRequestHandler<DeleteNoteRequest>
{
    public async Task Handle(DeleteNoteRequest request, CancellationToken cancellationToken)
    {
        var note = await repository.GetNoteByIdAsync(request.Id);
        if (note is null)
            throw new NotFoundException($"Note with id={request.Id}");
        
        await repository.DeleteNoteAsync(note);
    }
}