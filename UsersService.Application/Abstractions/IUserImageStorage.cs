namespace UsersService.Application.Abstractions;

public interface IUserImageStorage
{
    public Task<(string, string)> UploadAsync(string subjectId, Stream stream, string contentType, CancellationToken ct);
    public Task DelateAsync(string objectKey, CancellationToken ct);
}