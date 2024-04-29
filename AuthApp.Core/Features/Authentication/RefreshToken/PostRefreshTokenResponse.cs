namespace AuthApp.Core.Features.Authentication.RefreshToken
{
    public record PostRefreshTokenResponse(bool isAuthenticated ,string token, string refreshToken);

}