using ManagedCode.Storage.Client;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace ManagedCode.Storage.IntegrationTests.Tests;

[Collection(nameof(StorageTestApplication))]
public abstract class BaseControllerTests
{
    protected readonly StorageTestApplication TestApplication;
    protected readonly string ApiEndpoint;
    private static readonly string baseTestApi = "https://localhost:44332/";

    protected BaseControllerTests(StorageTestApplication testApplication, string apiEndpoint)
    {
        TestApplication = testApplication;
        ApiEndpoint = apiEndpoint;
    }

    protected HttpClient GetHttpClient(string baseAddress = null)
    {
        baseAddress ??= baseTestApi;
        return TestApplication.CreateClient(new WebApplicationFactoryClientOptions{ BaseAddress = new Uri(baseAddress)});
    }

    protected IStorageClient GetStorageClient()
    {
        return new Client.StorageClient(GetHttpClient());
    }
}