namespace InnoClinic.Shared.Configurations;

public static class JwtConfiguration
{
    public const string Issuer = "http://localhost:5246";
    public const string Audience = "http://localhost:5246";
    public const string SigningKey = "fuf";
}