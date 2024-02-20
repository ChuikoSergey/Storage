using ManagedCode.Communication;
using ManagedCode.Storage.Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebApiSample.Interfaces;

namespace WebApiSample.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AzureStorageController : Controller
{
    private readonly IAzureStorageService _azureStorageService;

    public AzureStorageController(IAzureStorageService azureStorageService)
    {
        _azureStorageService = azureStorageService;
    }
    
    [HttpPost("upload")]
    public async Task<ActionResult<Result<BlobMetadata>>> UploadFile(IFormFile formFile)
    {
        try
        {
            if (formFile == null || formFile.Length == 0)
            {
                return BadRequest("Invalid file.");
            }

            return Ok(await _azureStorageService.UploadFileAsync(formFile));
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error:{e.Message}");
        }
    }

    [HttpGet("download/{fileName}")]
    public async Task<IActionResult> DownloadFile(string fileName)
    {
        try
        {
            var result = await _azureStorageService.DownloadFileAsync(fileName);

            return File(result.Value.FileStream, "application/octet-stream", fileName);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e.Message}");
        }
    }
}