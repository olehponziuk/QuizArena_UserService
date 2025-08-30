namespace UsersService.Domain.Entities;

public class Rank
{
    public Guid Id { get;private set; }
    public int Value { get; private set; }
    
    public User User { get; private set; }
    public Guid UserId { get; private set; }
    
    

    public Rank()
    {
        Id = Guid.NewGuid();
    }
    
    public void ChangeRank(int sum)
    {
        if (Value < -sum)
            Value = 0;
        else
            Value += sum;
    }
    
}