namespace AuthApp.Core.Features.Authentication.Login
{
    public record PostLoginResponse(bool isAuthenticate,string token);
    
}