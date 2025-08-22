using UsersService.Domain.ValueObjects;

namespace UsersService.Application.Abstractions;

public interface ICurrentUser
{
    public bool IsAuthenticated { get; }
    public string? UserName { get; }
    public Email Email { get; }
    public string? SubjectId { get; }
    public IEnumerable<string> Roles { get; }
}