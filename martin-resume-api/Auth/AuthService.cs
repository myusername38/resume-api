using System;
namespace martin_resume_api.Auth;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using martin_resume_api.Entities;
using martin_resume_api.Repos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class AuthService
{

    private readonly IConfiguration _config;
    private readonly UserRepo _userRepo;
    
    public AuthService(
        IConfiguration config,
        UserRepo userRepo
    )
    {
        _config = config;
        _userRepo = userRepo;
    }

    public string GenerateJWTToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("Auth").GetValue<string>("JWT-Key")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(
            issuer: _config.GetSection("Auth").GetValue<string>("JWT-Issuer"),
            audience: _config.GetSection("Auth").GetValue<string>("JWT-Audience"),
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<User> Authenticate(string email, string password)
    {
        var user = await _userRepo.Users()
          .AsNoTracking()
          .FirstOrDefaultAsync(x => x.Email == email);

        if (user == null)
        {
            return null;
        }

        var authenticated = Encryption.VerifyHashedPassword(email, user.Password, password);
        if (!authenticated)
        {
            return null;
        }
        return user;
    }

}
