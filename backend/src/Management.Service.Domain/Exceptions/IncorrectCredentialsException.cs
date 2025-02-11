namespace Management.Service.Domain.Exceptions;

public class IncorrectCredentialsException: Exception
{
    public IncorrectCredentialsException()
    {
    }

    public IncorrectCredentialsException(string? message) : base(message)
    {
    }

    public IncorrectCredentialsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}