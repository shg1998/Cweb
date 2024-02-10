namespace Services.Services.Auth;

public interface IJwtService
{
    Task<string> GenerateAsync(Entities.User.User user);
}
