using HealthcareSystem.API.Helpers;
using HealthcareSystem.API.Repositories.Interfaces;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using HealthcareSystem.Models.Entities;

namespace HealthcareSystem.API.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly JwtHelper _jwt;
    private readonly ILogger<AuthService> _logger;
    // In-memory refresh token store (use Redis/DB in production)
    private static readonly Dictionary<string, (int UserId, DateTime Expiry)> _refreshTokens = new();

    public AuthService(IUserRepository users, JwtHelper jwt, ILogger<AuthService> logger)
    {
        _users = users;
        _jwt = jwt;
        _logger = logger;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        _logger.LogInformation("Login attempt for {Email}", dto.Email);

        var user = await _users.GetByEmailAsync(dto.Email);
        if (user == null || !user.IsActive)
        {
            _logger.LogWarning("Login failed - user not found: {Email}", dto.Email);
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Login failed - invalid password: {Email}", dto.Email);
            return null;
        }

        var token = _jwt.GenerateToken(user);
        var refreshToken = _jwt.GenerateRefreshToken();
        _refreshTokens[refreshToken] = (user.Id, DateTime.UtcNow.AddDays(7));

        _logger.LogInformation("Login successful for {Email}, Role: {Role}", user.Email, user.Role);

        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Role = user.Role,
            FullName = user.FullName,
            UserId = user.Id,
            Expiry = DateTime.UtcNow.AddHours(8)
        };
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        if (await _users.EmailExistsAsync(dto.Email))
        {
            _logger.LogWarning("Registration failed - email exists: {Email}", dto.Email);
            return null;
        }

        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email.ToLower(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role
        };

        await _users.AddAsync(user);
        _logger.LogInformation("New user registered: {Email}, Role: {Role}", user.Email, user.Role);

        var token = _jwt.GenerateToken(user);
        var refreshToken = _jwt.GenerateRefreshToken();
        _refreshTokens[refreshToken] = (user.Id, DateTime.UtcNow.AddDays(7));

        return new AuthResponseDto
        {
            Token = token,
            RefreshToken = refreshToken,
            Role = user.Role,
            FullName = user.FullName,
            UserId = user.Id,
            Expiry = DateTime.UtcNow.AddHours(8)
        };
    }

    public async Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenDto dto)
    {
        if (!_refreshTokens.TryGetValue(dto.RefreshToken, out var entry) ||
            entry.Expiry < DateTime.UtcNow)
            return null;

        var user = await _users.GetByIdAsync(entry.UserId);
        if (user == null || !user.IsActive) return null;

        _refreshTokens.Remove(dto.RefreshToken);
        var newToken = _jwt.GenerateToken(user);
        var newRefresh = _jwt.GenerateRefreshToken();
        _refreshTokens[newRefresh] = (user.Id, DateTime.UtcNow.AddDays(7));

        return new AuthResponseDto
        {
            Token = newToken,
            RefreshToken = newRefresh,
            Role = user.Role,
            FullName = user.FullName,
            UserId = user.Id,
            Expiry = DateTime.UtcNow.AddHours(8)
        };
    }
}
