using Domain.Entities;

namespace Application.Repositories;

public interface INoteRepository
{
    Task<IReadOnlyList<Note>> GetAllNotesAsync();
    Task<Note?> GetNoteByIdAsync(Guid id);
    Task AddNoteAsync(Note note);
    Task UpdateNoteAsync(Note note);
}