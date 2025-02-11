namespace Management.Service.Domain.Exceptions;

public class UserAlreadyExistsException: EntityAlreadyExistsException
{
    public UserAlreadyExistsException()
    {
    }

    public UserAlreadyExistsException(string? message) : base(message)
    {
    }

    public UserAlreadyExistsException(string? message, EntityAlreadyExistsException? innerException) : base(message, innerException)
    {
    }
}