using ManagedCode.Communication;
using ManagedCode.Storage.Core.Models;
using ManagedCode.Storage.FileSystem;
using ManagedCode.Storage.IntegrationTests.TestApp.Controllers.Base;
using ManagedCode.Storage.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagedCode.Storage.IntegrationTests.TestApp.Controllers;

[Route("files")]
[ApiController]
public class FilesTestController(IFileSystemStorage storage) : BaseTestController<IFileSystemStorage>(storage)
{
    [HttpPost("UploadStream")]
    public async Task<Result<BlobMetadata>> UploadStream(
        [FromBody] Stream streamFile,
        [FromQuery] UploadOptions? options = null
        )
    {
        return await Storage.UploadAsync(streamFile, options);
    }
    
    [HttpPost("UploadFile")]
    public async Task<Result<BlobMetadata>> UploadToFile(
        [FromBody] IFormFile formFile,
        [FromQuery] UploadOptions? options = null)
    {
        return await Storage.UploadToStorageAsync(formFile, options);
    }
}