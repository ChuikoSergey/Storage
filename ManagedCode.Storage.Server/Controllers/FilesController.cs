using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ManagedCode.Communication;
using ManagedCode.Storage.Core.Models;
using ManagedCode.Storage.FileSystem;
using ManagedCode.Storage.FileSystem.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagedCode.Storage.Server;

[ApiController]
[Route("files")]
public class FilesController : ControllerBase
{
    [HttpPost("upload")]
    public async Task<Result<BlobMetadata>> UploadStream(
        [FromBody] Stream streamFile,
        [FromQuery] UploadOptions? options = null)
    {
        FileSystemStorage Storage = new (new FileSystemStorageOptions{ BaseFolder = options?.Directory});
        return await Storage.UploadAsync(streamFile, options);
    }

    [HttpPost("upload")]
    public async Task<Result<BlobMetadata>> UploadFile(
        [FromBody] IFormFile formFile,
        [FromQuery] UploadOptions? options = null)
    {
        FileSystemStorage Storage = new (new FileSystemStorageOptions{ BaseFolder = options?.Directory});
        return await Storage.UploadToStorageAsync(formFile, options);
    }
}