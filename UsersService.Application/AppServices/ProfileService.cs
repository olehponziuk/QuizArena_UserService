using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersService.Application.Abstractions;
using UsersService.Application.DTOs;
using UsersService.Application.Exeptions;
using UsersService.Domain.Entities;

namespace UsersService.Application.Profiles.AppServices;

public sealed class ProfileService
{
    private readonly IAppDbContext _db;
    private readonly ICurrentUser _current;
    private readonly ILogger<ProfileService> _logger;

    public ProfileService(IAppDbContext db, ICurrentUser current, ILogger<ProfileService> logger)
    {
        _db = db;
        _current = current;
        _logger = logger;
    }

    public async Task<GetUserDto> CreateUser(CancellationToken ct)
    {
        User? user = await _db.Users.AsNoTracking()
            .FirstOrDefaultAsync(u=> u.SubjectId == _current.SubjectId, ct);
        
        if (user is not null)
        {
            _logger.LogInformation("User already exist!");
            throw new AlreadyExistsException("User Already Exists");
        }

        Rank rank = new Rank();
        user = new User(
            _current.UserName, 
            _current.Email,
            _current.SubjectId, 
            rank);

        await _db.Users.AddAsync(user, ct);
        await _db.Ranks.AddAsync(rank, ct);
        await _db.SaveChangesAsync(ct);

        return new GetUserDto(
            UserName : user.UserName,
            Rank : rank.Value,
            SubjectId : user.SubjectId
        );
    }
    public async Task<GetUserDto> ChangeUserName(string newUserName, CancellationToken ct)
    {
        var user = await _db.Users.Include(u=> u.Rank).Include(u=> u.UserImage).
            FirstOrDefaultAsync(u=> u.SubjectId == _current.SubjectId, ct);

        if (user is null)
        {
            _logger.LogInformation("User is not exists!");
            throw new KeyNotFoundException($"User with subId({_current!.SubjectId}) not found!");
        }
        
        user.SetUserName(newUserName);
        await _db.SaveChangesAsync(ct);

        return new GetUserDto(
            UserName: user.UserName,
            SubjectId: user.SubjectId,
            Rank: user.Rank.Value,
            ImageUrl: user.UserImage.Url
        );
    }

    public async Task<GetUserDto> AddRateToRank(int rate, CancellationToken ct)
    {
        var user = await _db.Users.Include(u=> u.Rank).
            FirstOrDefaultAsync(u=> u.SubjectId == _current.SubjectId, ct);
        
        if (user is null)
        {
            _logger.LogInformation("User is not exists!");
            throw new KeyNotFoundException($"User with subId({_current!.SubjectId}) not found!");
        }
        
        user.Rank.ChangeRank(rate);
        await _db.SaveChangesAsync(ct);
        
        return new GetUserDto(
            UserName: user.UserName,
            SubjectId: user.SubjectId,
            Rank: user.Rank.Value,
            ImageUrl: user.UserImage.Url
        ); 
    }

    /*public async Task<string> AddOrReplaceImage()
    {
        
    }*/

    public async Task<int> GetRank(CancellationToken ct)
    {
        var rank = await _db.Users
            .Where(u => u.SubjectId == _current.SubjectId)
            .Select(u => u.Rank)
            .FirstOrDefaultAsync(ct);

        if (rank is null)
        {
            _logger.LogInformation("User is not exists!");
            throw new KeyNotFoundException($"User with subId({_current!.SubjectId}) not found!");
        }
        
        return rank.Value;
    }

    public async Task<string?> GetImageUrl(CancellationToken ct) =>
        await _db.Users
            .Where(u => u.SubjectId == _current.SubjectId)
            .Select(u => u.UserImage.Url)
            .FirstOrDefaultAsync(ct);


    public async Task<GetUserDto> GetUserData(CancellationToken ct)
    {
        User? user = await _db.Users.Include(u=> u.Rank).Include(u=> u.UserImage)
            .FirstOrDefaultAsync(u=> u.SubjectId == _current.SubjectId, ct);
        
        if (user is null)
        {
            _logger.LogInformation("User is not exists!");
            throw new KeyNotFoundException($"User with subId({_current!.SubjectId}) not found!");

        }

        return new GetUserDto(
            UserName : user.UserName,
            Rank : user.Rank.Value,
            SubjectId : user.SubjectId,
            ImageUrl: user.UserImage.Url
        );
    }




}