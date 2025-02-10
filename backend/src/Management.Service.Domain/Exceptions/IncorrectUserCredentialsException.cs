namespace Management.Service.Domain.Exceptions;

public class IncorrectUserCredentialsException: Exception
{
    public IncorrectUserCredentialsException()
    {
    }

    public IncorrectUserCredentialsException(string? message) : base(message)
    {
    }

    public IncorrectUserCredentialsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}