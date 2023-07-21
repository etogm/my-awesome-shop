using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using MyAwesomeShop.Shared.Security.Domain;

namespace MyAwesomeShop.Shared.Security.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class SecurityController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    private readonly SecurityOptions _options;

    public SecurityController(UserManager<User> userManager, IOptions<SecurityOptions> options)
    {
        _userManager = userManager;
        _options = options.Value;
    }

    [HttpPost("token")]
    public async Task<IResult> CreateTokenAsync(CreateTokenRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Results.NotFound(request.Username);
        }

        var checkedResult = await _userManager.CheckPasswordAsync(user, request.Password);

        if (!checkedResult)
        {
            return Results.BadRequest(request.Password);
        }

        var token = await CreateTokenAsync(user);

        return Results.Ok(token);
    }

    [HttpPost("register")]
    public async Task<IResult> RegisterAsync(RegisterRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user != null)
        {
            return Results.BadRequest(request.Username);
        }

        var newUser = new User { UserName = request.Username, SecurityStamp = Guid.NewGuid().ToString() };
        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (!result.Succeeded)
        {
            return Results.StatusCode(500);
        }

        return Results.Ok();
    }

    [HttpPost("refreshToken")]
    public async Task<IResult> RefreshTokenAsync(RefreshTokenRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);

        if (user == null)
        {
            return Results.BadRequest(request.Username);
        }

        if (user.RefreshToken == null || user.RefreshToken.Value != request.RefreshToken)
        {
            return Results.BadRequest(request.RefreshToken);
        }

        var token = await CreateTokenAsync(user);
        return Results.Ok(token);
    }

    private async Task<CreateTokenResponse> CreateTokenAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        if (user.UserName != null)
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: DateTime.UtcNow.Add(_options.Expiry),
            claims: claims,
            signingCredentials: new SigningCredentials(_options.GetSigningKey(), SecurityAlgorithms.HmacSha256));

        user.RefreshToken = new RefreshToken(token.ValidTo);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new Exception("Не удалось сохранить");
        }

        var handler = new JwtSecurityTokenHandler();
        return new CreateTokenResponse(handler.WriteToken(token), user.RefreshToken.Value, _options.Expiry.Seconds);
    }
}

public record CreateTokenRequest(string Username, string Password);

public record CreateTokenResponse(string AccessToken, string? RefreshToken, double ExpiresIn);

public record RegisterRequest(string Username, string Password);

public record RefreshTokenRequest(string Username, string RefreshToken);
