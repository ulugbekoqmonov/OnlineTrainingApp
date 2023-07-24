namespace Application.Exceptions;

public class AllreadyExistsException:Exception
{
    public AllreadyExistsException()
            : base() { }

    public AllreadyExistsException(string name, string key)
        : base($"Entity \"{name}\" ({key}) already exists") { }
}
