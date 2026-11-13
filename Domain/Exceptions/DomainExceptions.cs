namespace Domain.Exceptions;

public abstract class DomainException : Exception
{
    public DomainException(string message) 
        : base(message) { }
}

public class NoteContentEmptyException : DomainException
{
    public NoteContentEmptyException()
        : base("Note content is empty.") { }
}