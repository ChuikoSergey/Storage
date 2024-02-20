using ManagedCode.Communication;
using ManagedCode.Storage.Core.Models;

namespace WebApiSample.Interfaces;

public interface IAzureStorageService
{
    Task<Result<BlobMetadata>> UploadFileAsync(IFormFile formFile);
    Task<Result<LocalFile>> DownloadFileAsync(string fileName);
}