using Microsoft.Extensions.Logging;
using UsersService.Application.Abstractions;

namespace UsersService.Application.Profiles.AppServices;

public class FriendshipService
{
    private readonly ICurrentUser _current;
    private readonly ILogger<FriendshipService> _logger;
    private readonly IAppDbContext _db;
    
    public FriendshipService(IAppDbContext db, ICurrentUser current, ILogger<FriendshipService> logger)
    {
        _db = db;
        _current = current;
        _logger = logger;
    }
}