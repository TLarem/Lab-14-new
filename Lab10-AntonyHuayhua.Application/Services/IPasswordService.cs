namespace Lab10_AntonyHuayhua.Application.Port.Services;

public interface IPasswordService
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
