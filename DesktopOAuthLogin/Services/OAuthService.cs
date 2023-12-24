using DesktopOAuthLogin.Models;
using Flurl;
using Flurl.Http;

namespace DesktopOAuthLogin.Services;

public class OAuthService : IOAuthService
{
    private readonly string clientId = "f4ae8af1-b862-4b06-b7e3-16e75da06f84";
    private readonly string scope = "Xboxlive.signin Xboxlive.offline_access";
    private readonly string loginUri = "https://login.live.com/oauth20_authorize.srf";
    private readonly string tokenUri = "https://login.live.com/oauth20_token.srf";
    private readonly string redirectUri = "https://login.live.com/oauth20_desktop.srf";

    public async Task<AccessTokenResponse> Login()
    {
        var authorizationCode = await GetAuthorizationCode();
        return await GetAccessCode(authorizationCode);
    }

    private Task<string> GetAuthorizationCode()
    {
        var taskCompletionSource = new TaskCompletionSource<string>();
        string? authorizationCode = null;
        var loginWindow = new LoginWindow();

        loginWindow.webView.CoreWebView2InitializationCompleted += (sender, args) =>
        {
            loginWindow.webView.CoreWebView2.CookieManager.DeleteAllCookies();
        };

        loginWindow.webView.ContentLoading += (sender, args) =>
        {
            var url = new Url(loginWindow.webView.Source);
            authorizationCode = url.QueryParams.FirstOrDefault("code")?.ToString();
            if (url.ToString().StartsWith(redirectUri) && !string.IsNullOrEmpty(authorizationCode))
            {
                loginWindow.Close();
            }
        };

        loginWindow.webView.Source = loginUri
            .SetQueryParams(
                new
                {
                    scope,
                    client_id = clientId,
                    redirect_uri = redirectUri,
                    response_type = "code",
                    approval_prompt = "auto",
                }
            )
            .ToUri();

        loginWindow.Closed += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(authorizationCode))
            {
                taskCompletionSource.SetResult(authorizationCode);
            }
            else
            {
                taskCompletionSource.SetCanceled();
            }
        };

        loginWindow.Show();

        return taskCompletionSource.Task;
    }

    private async Task<AccessTokenResponse> GetAccessCode(string authorizationCode)
    {
        var response = await tokenUri.PostUrlEncodedAsync(
            new
            {
                scope,
                client_id = clientId,
                redirect_uri = redirectUri,
                code = authorizationCode,
                grant_type = "authorization_code",
            }
        );
        return await response.GetJsonAsync<AccessTokenResponse>();
    }
}
