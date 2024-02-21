using Azure.Storage.Blobs;
using ManagedCode.Communication;
using ManagedCode.Storage.Azure;
using ManagedCode.Storage.Core.Models;
using Microsoft.AspNetCore.Http;
using Moq;
using WebApiSample.Services;

namespace WebApiSample.Tests.Services;

public class AzureStorageServiceTest
{
    [Fact]
    public async Task UploadFileAsync_Success()
    {
        // Arrange
        var mockContainer = new Mock<BlobContainerClient>();
        var mockStorage = new Mock<IAzureStorage>();
        var mockFormFile = new Mock<IFormFile>();
        var service = new AzureStorageService(mockStorage.Object);
        
        mockFormFile.Setup(x => x.OpenReadStream()).Returns(new MemoryStream(new byte[] { 1, 2, 3, 4, 5 }));
        mockFormFile.Setup(x => x.FileName).Returns("test.jpg");
        mockStorage.Setup(x => x.StorageClient).Returns(mockContainer.Object);
        mockStorage.Setup(x => x.UploadAsync(It.IsAny<Stream>(),
                It.IsAny<UploadOptions>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<BlobMetadata>.Succeed(GenerateBlobMetadata()));
        
        // Act
        var result = await service.UploadFileAsync(mockFormFile.Object);
        
        // Assert
        Assert.True(result.IsSuccess);
    }

    private static BlobMetadata GenerateBlobMetadata()
    {
        return new BlobMetadata()
        {
            FullName = Guid.NewGuid() + ".txt",
            Name = "b2665f95-0ee0-42c1-9148-331cca802185.jpg",
            Uri = new Uri("https://test.blob.core.windows.net/test/b2665f95-0ee0-42c1-9148-331cca802185.jpg"),
            Metadata = new Dictionary<string, string>(),
            LastModified = DateTime.MinValue,
            CreatedOn = DateTime.MinValue,
            Container = "test",
            MimeType = "application/octet-stream\"",
            Length = 720037
        };
    }
}