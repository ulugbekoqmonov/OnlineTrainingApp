namespace Application.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException() : base() { }
    public NotFoundException(string name, object? key = null):base($"{name} ({key}) Not Found") { }
}
