namespace UsersService.Domain.Entities;

public class UserImage
{
    public Guid Id { get; private set; }
    public string Url { get; private set; }
    
    private UserImage() {}

    public UserImage(string url)
    {
        Id = Guid.NewGuid();
        SetUrl(url);
    }

    public void SetUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Url hash cannot be empty", nameof(url));
        Url = url;
    }
}