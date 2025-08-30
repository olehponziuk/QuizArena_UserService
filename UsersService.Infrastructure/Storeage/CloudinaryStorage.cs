using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using UsersService.Application.Abstractions;

namespace UsersService.Infrastructure.Storeage;

public class CloudinaryOptions
{
    public string CloudName { get; init; } = default!;
    public string ApiKey { get; init; } = default!;
    public string ApiSecret { get; init; } = default!;
    public string Folder { get; init; } = "avatars";
    public long MaxBytes { get; init; } = 5 * 1024 * 1024;
}    

public class CloudinaryStorage: IUserImageStorage
{
    private readonly CloudinaryOptions _opt;
    private readonly Cloudinary _cloud;

    public CloudinaryStorage(IOptions<CloudinaryOptions> opt)
    {
        _opt = opt.Value;
        _cloud = new Cloudinary(new Account(_opt.CloudName, _opt.ApiKey, _opt.ApiSecret))
            { Api = { Secure = true}};
    }
    
    public async Task<(string, string)> UploadAsync(string subjectId, Stream stream, string contentType, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(subjectId))
            throw new ArgumentException("Wrong subId", nameof(subjectId));
        
        if (stream == null || stream.Length == null)
            throw new ArgumentException("File is empty!");

        if (stream.Length > _opt.MaxBytes)
            throw new ArgumentException("Image too big!");


        string publicId = $"{_opt.Folder}/{subjectId}/{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription($"{subjectId}", stream ),
            PublicId = publicId,
            Overwrite = true,
            Invalidate = true,
            Transformation = new Transformation()
                .Gravity("auto").Crop("fill")
                .Width(512).Height(512)
                .FetchFormat("auto").Quality("auto:good")
        };
        
        ct.ThrowIfCancellationRequested();
        var response = await _cloud.UploadAsync(uploadParams, ct);

        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            throw new InvalidOperationException("Upload failed!");

        return (response.PublicId, response.Url.ToString());
    }

    public async Task DelateAsync(string objectKey, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(objectKey))
            throw new ArgumentException("Wrong objectId", nameof(objectKey));
        var delParams = new DeletionParams(objectKey){Invalidate = true};
        await _cloud.DestroyAsync(delParams);
    }
}