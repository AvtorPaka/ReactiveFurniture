using FluentValidation;
using Management.Service.Domain.Contracts.Dal.Entities;
using Management.Service.Domain.Contracts.Dal.Interfaces;
using Management.Service.Domain.Exceptions;
using Management.Service.Domain.Mappers;
using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Hasher;
using Management.Service.Domain.Services.Interfaces;
using Management.Service.Domain.Validators;
using Microsoft.Extensions.Logging;

namespace Management.Service.Domain.Services;

public class UserCredentialsService : IUserCredentialsService
{
    private readonly ICredentialsRepository _credentialsRepository;
    private readonly ILogger<UserCredentialsService> _logger;

    public UserCredentialsService(ICredentialsRepository credentialsRepository, ILogger<UserCredentialsService> logger)
    {
        _credentialsRepository = credentialsRepository;
        _logger = logger;
    }

    public async Task RegisterNewUser(RegisterUserModel registerModel, CancellationToken cancellationToken)
    {
        try
        {
            await RegisterNewUserUnsafe(registerModel, cancellationToken);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "{time} | Invalid request parameters during RegisterNewUser call.", DateTime.Now);
            throw new DomainException("Invalid request parameters.", ex);
        }
        catch (EntityAlreadyExistsException ex)
        {
            //TODO: Remember to add
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during GetFurnitureGoods call.", DateTime.Now);
            throw;
        }
    }

    private async Task RegisterNewUserUnsafe(RegisterUserModel registerModel, CancellationToken cancellationToken)
    {
        var validator = new RegisterUserModelValidator();
        await validator.ValidateAndThrowAsync(registerModel, cancellationToken);

        using var transaction = _credentialsRepository.CreateTransactionScope();

        await _credentialsRepository.AddUserCredentials(
            entity: registerModel.MapModelToEntity(),
            cancellationToken: cancellationToken
        );

        transaction.Complete();
    }

    public async Task<SetCookieModel> LoginUser(LoginUserModel loginModel, CancellationToken cancellationToken)
    {
        try
        {
            return await LoginUserUnsafe(loginModel, cancellationToken);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, "{time} | Invalid request parameters during RegisterNewUser call.", DateTime.Now);
            throw new DomainException("Invalid request parameters.", ex);
        }
        catch (IncorrectUserCredentialsException ex)
        {
            //TODO: Remember to add
            throw;
        }
        catch (EntityNotFoundException ex)
        {
            //TODO: Remember to add
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during GetFurnitureGoods call.", DateTime.Now);
            throw;
        }
    }

    private async Task<SetCookieModel> LoginUserUnsafe(LoginUserModel loginModel, CancellationToken cancellationToken)
    {
        var validator = new LoginUserModelValidator();
        await validator.ValidateAndThrowAsync(loginModel, cancellationToken);

        var userEntity = await _credentialsRepository.GetUser(
            userEmail: loginModel.Email,
            cancellationToken: cancellationToken
        );

        if (!PasswordHasher.Verify(loginModel.Password, userEntity.Password))
        {
            throw new IncorrectUserCredentialsException("Incorrect password credentials.");
        }

        using var transaction = _credentialsRepository.CreateTransactionScope();

        string sessionId = GenerateRandomSessionId();
        DateTimeOffset expirationDate = DateTimeOffset.UtcNow.AddHours(12);

        await _credentialsRepository.CreateUserSession(
            entity: new UserSessionEntity
            {
                UserId = userEntity.Id,
                Id = sessionId,
                ExpirationDate = expirationDate
            },
            cancellationToken: cancellationToken
        );

        transaction.Complete();

        return new SetCookieModel(
            SessionId: sessionId,
            ExpirationDate: expirationDate
        );
    }

    private static string GenerateRandomSessionId()
    {
        Guid sessionGuid = Guid.NewGuid();

        return sessionGuid.ToString();
    }

    public async Task LogoutUser(LogoutUserModel logoutModel, CancellationToken cancellationToken)
    {
        try
        {
            await LogoutUserUnsafe(logoutModel, cancellationToken);
        }
        catch (EntityNotFoundException ex)
        {
            //TODO: Remember to add
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during GetFurnitureGoods call.", DateTime.Now);
            throw;
        }
    }

    private async Task LogoutUserUnsafe(LogoutUserModel logoutModel, CancellationToken cancellationToken)
    {
        using var transaction = _credentialsRepository.CreateTransactionScope();

        await _credentialsRepository.DeleteUserSession(
            sessionId: logoutModel.SessionId,
            cancellationToken: cancellationToken
        );

        transaction.Complete();
    }
}