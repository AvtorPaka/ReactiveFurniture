namespace Management.Service.Domain.Exceptions;

public class UserUnauthenticatedException: Exception
{
    public UserUnauthenticatedException()
    {
    }

    public UserUnauthenticatedException(string? message) : base(message)
    {
    }

    public UserUnauthenticatedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}