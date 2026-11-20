namespace Lab10_AntonyHuayhua.Infrastructure.Configuration;

public class JwtSettings
{
    public string Secret { get; set; } = "ClaveSecretaSuperSeguraParaLab10LukasMelgar2026!";
    public string Issuer { get; set; } = "lab10-api";
    public string Audience { get; set; } = "lab10-clients";
    public int ExpirationMinutes { get; set; } = 120;
}
