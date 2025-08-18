using UsersService.Domain.Enums;

namespace UsersService.Domain.Entities;

public class Friendship
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? AcceptedAt { get; private set; } = null;

    public Guid RequesterId { get; private set; }
    public User Requester { get; private set; }
    
    public Guid AddresseeId { get; private set; }
    public User Addressee { get; private set; }
    
    public FriendStatus Status { get; private set; }
    
    private Friendship(){}

    public Friendship(DateTime createdAt, Guid requesterId, Guid addresseeId)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        RequesterId = requesterId;
        AddresseeId = addresseeId;
        Status = FriendStatus.Pending;
    }

    public void Accept()
    {
        AcceptedAt = DateTime.UtcNow;
        Status = FriendStatus.Accepted;
    }

    public void Block()
    {
        AcceptedAt = null;
        Status = FriendStatus.Blocked;
    }
}