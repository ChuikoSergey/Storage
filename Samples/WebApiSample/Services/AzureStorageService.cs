using ManagedCode.Communication;
using ManagedCode.Storage.Azure;
using ManagedCode.Storage.Core.Models;
using WebApiSample.Interfaces;

namespace WebApiSample.Services;

public class AzureStorageService : IAzureStorageService
{
    private readonly IAzureStorage _storage;

    public AzureStorageService(IAzureStorage storage)
    {
        _storage = storage;
    }
    
    public async Task<Result<BlobMetadata>> UploadFileAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
       
        var result = await _storage.UploadAsync(stream, options =>
        {
            options.FileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        });
        
        return await _storage.GetBlobMetadataAsync(result.Value.Name);
    }

    public async Task<Result<LocalFile>> DownloadFileAsync(string fileName)
    {
        var blob = await _storage.DownloadAsync(fileName);
        
        return blob;
    }
}