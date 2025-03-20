using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InnoClinic.Application.IService;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Configurations;
using InnoClinic.Shared.Constants;
using Microsoft.IdentityModel.Tokens;

namespace InnoClinic.Application.Service;

public class TokenService : ITokenService
{
    public string CreateToken(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, user.Id),
            new(CustomClaimTypes.Email, user.Email),
            new(CustomClaimTypes.Username, user.UserName),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(CustomClaimTypes.Role, role));
        }
        
        var key = SHA256.HashData(Encoding.UTF8.GetBytes(JwtConfiguration.SigningKey));
        var secret = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddHours(JwtConfiguration.ExpiresHours),
            SigningCredentials = credentials,
            Issuer = JwtConfiguration.Issuer,
            Audience = JwtConfiguration.Audience,
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}