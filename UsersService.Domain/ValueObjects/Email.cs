namespace UsersService.Domain.ValueObjects;

public class Email
{
    public string Value { get; private set; }
    
    private Email() {}

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be empty", nameof(value));

        Value = value.Trim().ToLower();
    }
}