using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InnoClinic.Application.IService;
using InnoClinic.BusinessLogic.Entities;
using InnoClinic.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace InnoClinic.Application.Service;

public class TokenService(
    IConfiguration config
) : ITokenService
{
    public string CreateToken(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(CustomClaimTypes.Username, user.UserName),
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var a = Encoding.UTF8.GetBytes(config["Jwt:SigningKey"]);
        var b = SHA256.HashData(a);
        
        var c = Convert.ToBase64String(a);
        var d = Convert.ToBase64String(b);
        
        var key = b;
        var secret = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = config["JWT:Issuer"],
            Audience = config["JWT:Audience"],
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        
        return tokenHandler.WriteToken(token);
    }
}