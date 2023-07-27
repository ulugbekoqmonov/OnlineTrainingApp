namespace Application.Exceptions;

public class InvalidPasswordException:Exception
{
    public InvalidPasswordException() : base() { }
    public InvalidPasswordException(string message):base("Invalid password")
    {            
    }
}
