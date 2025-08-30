using UsersService.Application.Abstractions;
using UsersService.Domain.ValueObjects;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;


namespace UsersService.Infrastructure.Identity;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity.IsAuthenticated ?? false;

    public string? UserName =>
        _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
    public Email? Email
    {
        get
        {
            string? strEmail = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.Email).Value;
            if (string.IsNullOrWhiteSpace(strEmail))
                return null;
            else
                return new Email(strEmail);
        }
    }

    public string? SubjectId =>
        _httpContextAccessor?.HttpContext?.User?.FindFirst("sub")?.Value;

    public IEnumerable<string> Roles =>
        _httpContextAccessor?.HttpContext?.User?.FindAll(ClaimTypes.Role)?.Select(r => r.Value)
        ?? Enumerable.Empty<string>();
}