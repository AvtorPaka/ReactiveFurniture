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
            _logger.LogError(ex, "{time} | User with the same credential: {cred} already exists", DateTime.Now,
                registerModel.Email);
            throw new UserAlreadyExistsException(
                $"User with the same credential: {registerModel.Email} already exists.", ex);
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
        catch (IncorrectCredentialsException ex)
        {
            _logger.LogError(ex, "{time} | Incorrect password credentials provided to login.", DateTime.Now);
            throw;
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogError(ex, "{time} | User with credentials: {email} could not be found.", DateTime.Now,
                loginModel.Email);
            throw new UserNotFoundException($"User with credentials: {loginModel.Email} could not be found.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during GetFurnitureGoods call.", DateTime.Now);
            throw;
        }
    }

    private async Task<SetCookieModel> LoginUserUnsafe(LoginUserModel loginModel, CancellationToken cancellationToken)
    {
        var userEntity = await _credentialsRepository.GetUserByEmail(
            userEmail: loginModel.Email,
            cancellationToken: cancellationToken
        );

        if (!PasswordHasher.Verify(loginModel.Password, userEntity.Password))
        {
            throw new IncorrectCredentialsException("Incorrect credentials provided.");
        }

        using var transaction = _credentialsRepository.CreateTransactionScope();

        string sessionId = GenerateRandomSessionId();
        DateTimeOffset expirationDate = DateTimeOffset.UtcNow.AddHours(1);

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
            Username: userEntity.Username,
            Email: userEntity.Email,
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

    public async Task<SetCookieModel> CheckUserAuth(string? sessionId, CancellationToken cancellationToken)
    {
        try
        {
            return await CheckUserAuthUnsafe(sessionId, cancellationToken);
        }
        catch (UserUnauthenticatedException ex)
        {
            _logger.LogInformation(ex, "{time} | User was unauthenticated with presented credentials: {session-id}",
                DateTime.Now, sessionId ?? "none");
            throw;
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogError(ex, "{time} | User associated with credentials: {sessionID} could not be found.",
                DateTime.Now, sessionId ?? "");
            throw new UserNotFoundException($"User associated with credentials: {sessionId} could not be found.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{time} | Unexpected exception occured during user session authorization.",
                DateTime.Now);
            throw;
        }
    }


    private async Task<SetCookieModel> CheckUserAuthUnsafe(string? sessionId, CancellationToken cancellationToken)
    {
        if (sessionId == null)
        {
            throw new UserUnauthenticatedException("Credentials were not provided.");
        }

        IReadOnlyList<UserSessionEntity> sessionEntityList = await _credentialsRepository.GetSessionCredentials(
            sessionId: sessionId!,
            cancellationToken: cancellationToken
        );


        if (sessionEntityList.Count == 0)
        {
            throw new UserUnauthenticatedException("Invalid credentials.");
        }

        UserSessionEntity sessionEntity = sessionEntityList[0];
        if (DateTimeOffset.UtcNow > sessionEntity.ExpirationDate)
        {
            throw new UserUnauthenticatedException("Credentials expired.");
        }

        UserCredentialEntity userEntity = await _credentialsRepository.GetUserBySessionId(
            sessionId: sessionId,
            cancellationToken: cancellationToken
        );

        return new SetCookieModel(
            Username: userEntity.Username,
            Email: userEntity.Email,
            SessionId: sessionEntity.Id,
            ExpirationDate: sessionEntity.ExpirationDate
        );
    }
}