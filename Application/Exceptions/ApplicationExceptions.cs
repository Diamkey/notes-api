namespace Application.Exceptions;

public abstract class ApplicationException(string message)
    : Exception(message);
    
public class NotFoundException(string item)
    : ApplicationException($"{item} not found");