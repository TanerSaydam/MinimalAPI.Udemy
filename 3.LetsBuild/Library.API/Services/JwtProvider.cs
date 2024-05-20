using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Library.API.Services;

public sealed class JwtProvider
{
    public string CreateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my secret key my secret key my secret key my secret key my secret key my secret key "));

        JwtSecurityToken jwtSecurityToken = new(
            issuer: "Taner Saydam",
            audience: "Taner Saydam",
            claims: null,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMonths(1),
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
