using System;
namespace ResumeApi.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResumeApi.Models;
using ResumeApi.Repos;

public interface IAuthService
{
    public string GenerateJWTToken(User user);
    public Task<ApplicationUser> Register(string email, string username, string password);
    public Task<ApplicationUser> Authenticate(string email, string password);
    public Task<string> GetEamilVerificationCode(ApplicationUser user);
    public Task<string> GetChangePasswordCode(ApplicationUser user);
    public Task<IdentityResult> VerifyEmailAddress(ApplicationUser user, string token);
    public Task<IdentityResult> UpdateUserPassword(ApplicationUser user, string token, string newPassword);
}

public class AuthService: IAuthService
{
    private readonly IConfiguration _config;
    private readonly UserRepo _userRepo;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(
        IConfiguration config,
        UserRepo userRepo,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager
    )
    {
        _config = config;
        _userRepo = userRepo;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public string GenerateJWTToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("JWT").GetValue<string>("SecretKey")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        var token = new JwtSecurityToken(
            issuer: _config.GetSection("JWT").GetValue<string>("Issuer"),
            audience: _config.GetSection("JWT").GetValue<string>("Audience"),
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<ApplicationUser> Authenticate(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null && await _userManager.CheckPasswordAsync(user, password))
        {
            return user;
        }
        return null;
    }

    public async Task<ApplicationUser> Register(string email, string username, string password)
    {
        var userExists = await _userManager.FindByNameAsync(username);
        if (userExists != null)
        {
            throw new Exception("User already exists");
        }
        var emailExists = await _userManager.FindByEmailAsync(email);
        if (emailExists != null)
        {
            throw new Exception("Email Already exists");
        }
        ApplicationUser user = new ApplicationUser()
        {
            Email = email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = username,
        };
        var result = await _userManager.CreateAsync(user, password);
        return user;
    }

    public async Task<string> GetEamilVerificationCode(ApplicationUser user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task<string> GetChangePasswordCode(ApplicationUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IdentityResult> VerifyEmailAddress(ApplicationUser user, string token)
    {
        return await _userManager.ConfirmEmailAsync(user, token);
    }

    public async Task<IdentityResult> UpdateUserPassword(ApplicationUser user, string token, string newPassword)
    {
        return await _userManager.ResetPasswordAsync(user, token, newPassword);
    }
}
