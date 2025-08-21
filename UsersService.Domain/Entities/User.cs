using UsersService.Domain.ValueObjects;

namespace UsersService.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string UserName { get; private set; }
    public Email Email { get; private set; }
    public string? PassHash { get; private set; }

    public Rank Rank { get; private set; }
    public UserImage UserImage { get; private set; }
    public IReadOnlyCollection<Friendship> Friendships => _friendships.AsReadOnly();
    private readonly List<Friendship> _friendships = new();

    private User() { }

    public User(string userName, Email email, string passHash, Rank rank, UserImage image)
    {
        Id = Guid.NewGuid();
        SetUserName(userName);
        SetEmail(email);
        PassHash = passHash ?? throw new ArgumentNullException(nameof(passHash));
        Rank = rank ?? throw new ArgumentNullException(nameof(rank));
        UserImage = image ?? throw new ArgumentNullException(nameof(image));
    }

    public void SetUserName(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("Username cannot be empty", nameof(userName));

        UserName = userName;
    }

    public void SetEmail(Email email)
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public void ChangePassword(string newPassHash)
    {
        if (string.IsNullOrWhiteSpace(newPassHash))
            throw new ArgumentException("Password hash cannot be empty", nameof(newPassHash));

        PassHash = newPassHash;
    }

    public void ChangeRank(Rank rank)
    {
        Rank = rank ?? throw new ArgumentNullException(nameof(rank));
    }

    public void ChangeUserImage(UserImage image)
    {
        UserImage = image ?? throw new ArgumentNullException(nameof(image));
    }
}