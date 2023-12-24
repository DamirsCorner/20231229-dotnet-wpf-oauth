using DesktopOAuthLogin.Models;

namespace DesktopOAuthLogin.Services;

public interface IOAuthService
{
    Task<AccessTokenResponse> Login();
}
