using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BillsControl.ApplicationCore.Abstract.Auth;
using BillsControl.ApplicationCore.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BillsControl.Infrastructure.AuthHelpers;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public string GenerateToken(UserEntity userEntity)
    {
        Claim[] claims = [
            new(ClaimTypes.NameIdentifier, userEntity.Id.ToString()),
            new(ClaimTypes.Role, userEntity.Role.ToString())];
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(options.Value.ExpiresMinutes));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}