using Management.Service.Domain.Models;

namespace Management.Service.Domain.Services.Interfaces;

public interface IUserCredentialsService
{
    public Task RegisterNewUser(RegisterUserModel registerModel, CancellationToken cancellationToken);

    public Task<SetCookieModel> LoginUser(LoginUserModel loginModel, CancellationToken cancellationToken);

    public Task LogoutUser(LogoutUserModel logoutModel, CancellationToken cancellationToken);

    public Task<SetCookieModel> CheckUserAuth(string? sessionId, CancellationToken cancellationToken);
}