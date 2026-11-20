using Lab10_AntonyHuayhua.Application.Port.Services;

namespace Lab10_AntonyHuayhua.Infrastructure.Adapters.Services.Implementation;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
