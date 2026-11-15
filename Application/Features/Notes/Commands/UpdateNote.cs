using Application.Exceptions;
using Application.Repositories;
using MediatR;

namespace Application.Features.Notes.Commands;

public record UpdateNoteRequest(Guid Id, string Content)
    : IRequest;
    
internal sealed class UpdateNoteRequestHandler(INoteRepository repository)
    : IRequestHandler<UpdateNoteRequest>
{
    public async Task Handle(UpdateNoteRequest request, CancellationToken cancellationToken)
    {
        var note = await repository.GetNoteByIdAsync(request.Id);
        if (note is null)
            throw new NoteByIdNotFoundException(request.Id);
        
        note.Update(request.Content);
        
        await repository.UpdateNoteAsync(note);
    }
}