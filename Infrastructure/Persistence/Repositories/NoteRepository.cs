using Application.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class NoteRepository(ApplicationDbContext context) 
    : INoteRepository
{
    public async Task<IReadOnlyList<Note>> GetAllNotesAsync()
    {
        var notes = await context.Notes.AsNoTracking().ToListAsync();
        return notes;
    }

    public async Task<Note?> GetNoteByIdAsync(Guid id)
    {
        var note = await context.Notes.FirstOrDefaultAsync(note => note.Id == id);
        return note;
    }

    public async Task AddNoteAsync(Note note)
    {
        context.Notes.Add(note);
        await context.SaveChangesAsync();
    }
}