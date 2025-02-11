namespace Management.Service.Domain.Exceptions;

public class UserNotFoundException: EntityNotFoundException
{
    public UserNotFoundException()
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, EntityNotFoundException? innerException) : base(message, innerException)
    {
    }
}