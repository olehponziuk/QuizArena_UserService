namespace UsersService.Application.DTOs;

public sealed record GetUserDto(
    string UserName,
    string SubjectId,
    int Rank,
    string? ImageUrl = null);

