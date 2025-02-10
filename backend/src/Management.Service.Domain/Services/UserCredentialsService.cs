using Management.Service.Domain.Models;
using Management.Service.Domain.Services.Interfaces;

namespace Management.Service.Domain.Services;

public class UserCredentialsService : IUserCredentialsService
{
    public async Task RegisterNewUser(RegisterUserModel registerModel, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task<SetCookieModel> LoginUser(LoginUserModel loginModel, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }

    public async Task LogoutUser(LogoutUserModel logoutModel, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromMicroseconds(1), cancellationToken);
        throw new NotImplementedException();
    }
}