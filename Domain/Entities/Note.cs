using Domain.Exceptions;

namespace Domain.Entities;

public class Note
{
    // EF Core
    private Note() { }

    private Note(Guid id, string content, DateTimeOffset createdAt)
    {
        Id = id;
        Content = content;
        CreatedAt = createdAt;
    }
    
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public static Note Create(string content, TimeProvider? timeProvider = null)
    {
        Guid id = Guid.NewGuid();
        
        content = content.Trim();
        if (content == string.Empty)
            throw new NoteContentEmptyException();

        DateTimeOffset createdAt = timeProvider?.GetUtcNow() ?? DateTimeOffset.UtcNow;

        return new Note(id, content, createdAt);
    }
}