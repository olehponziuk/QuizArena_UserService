namespace UsersService.Domain.Entities;

public class Rank
{
    public Guid Id { get;private set; }
    public int Value { get; private set; } = 1000;
    
    public User User { get; private set; }
    
    private Rank(){}
    
    public void ChangeRank(int sum)
    {
        if (Value < -sum)
            Value = 0;

        Value += sum;
    }
    
}