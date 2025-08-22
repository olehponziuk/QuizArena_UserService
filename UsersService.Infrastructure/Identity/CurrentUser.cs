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
        _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name).ToString();
    public Email Email { get; }//continue
    public string? SubjectId { get; }
    public IEnumerable<string> Roles { get; }
}