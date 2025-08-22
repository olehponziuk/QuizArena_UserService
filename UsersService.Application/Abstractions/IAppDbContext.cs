using Microsoft.EntityFrameworkCore;
using UsersService.Domain.Entities;

namespace UsersService.Application.Abstractions;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<UserImage> UserImages { get; set; }
    public DbSet<Friendship> Friendships { get; set; }
    public DbSet<Rank> Ranks { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}