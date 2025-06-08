using APITrip.Models.Auth;
using APITrip.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace APITrip.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Title = model.Title,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Username
            };

            IdentityResult result;
            try
            {
                result = await _userManager.CreateAsync(user, model.Password);
            }
            catch (DbUpdateException ex)
            {
                // Affiche le détail dans la console de sortie de Visual Studio
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Erreur base de données : " + (ex.InnerException?.Message ?? ex.Message) });
            }

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = $"User creation failed: {errors}" });
            }

            // Assign default role
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            await _userManager.AddToRoleAsync(user, "User");

            return Ok(new Response { Status = "Success", Message = "User created successfully with default role!" });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new Response { Status = "Error", Message = "Invalid credentials!" });

            var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = GenerateToken(authClaims);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            // Correction : utiliser DateTime.UtcNow pour PostgreSQL
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Erreur base de données : " + (ex.InnerException?.Message ?? ex.Message) });
            }

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }


    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] TokenModel tokenModel)
    {
        try
        {
            if (tokenModel == null)
                return BadRequest("Invalid client request");

            var principal = GetPrincipalFromExpiredToken(tokenModel.AccessToken);
            if (principal == null)
                return BadRequest("Invalid access token or refresh token");

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);
            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid access token or refresh token");

            var newAccessToken = GenerateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Erreur base de données : " + (ex.InnerException?.Message ?? ex.Message) });
            }

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
        }
    }

    private JwtSecurityToken GenerateToken(IEnumerable<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddMinutes(5),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }
}
