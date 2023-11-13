using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace backend.helpers;

public static class JwtHelper
{
    public static string GenerarJwt(UserDto user, IConfiguration configuration)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var byteKey = Encoding.UTF8
        .GetBytes(configuration.GetSection("AppSettings:Token").Value!);


        var TokenDes = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, user.Rol),
                    new Claim(ClaimTypes.Sid, user.Id.ToString())

                }),
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(byteKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
        };

        var token = tokenHandler.CreateToken(TokenDes);

        return tokenHandler.WriteToken(token);

    }

    /* public static IEnumerable<Claim> ObtenerClaims(string jwtToken)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwtToken);
        IEnumerable<Claim> claims = securityToken.Claims;
        return claims;
    } */
}