namespace Application.Exceptions;

public class AllreadyExistsException:Exception
{
    public AllreadyExistsException()
            : base() { }

    public AllreadyExistsException(string name, object? key = null)
        : base($"Entity \"{name}\" ({key}) already exists") { }
}
