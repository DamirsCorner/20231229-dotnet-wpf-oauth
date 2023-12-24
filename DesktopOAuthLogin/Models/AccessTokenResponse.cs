using System.Text.Json.Serialization;

namespace DesktopOAuthLogin.Models;

public class AccessTokenResponse(
    string tokenType,
    int expiresIn,
    string scope,
    string accessToken,
    string refreshToken,
    string userId
)
{
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = tokenType;

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; } = expiresIn;

    public string Scope { get; set; } = scope;

    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = accessToken;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = refreshToken;

    [JsonPropertyName("user_id")]
    public string UserId { get; set; } = userId;
}
