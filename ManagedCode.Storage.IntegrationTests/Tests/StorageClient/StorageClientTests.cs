using ManagedCode.Storage.IntegrationTests.Constants;

namespace ManagedCode.Storage.IntegrationTests.Tests.StorageClient;

public class StorageClientTests(StorageTestApplication testApplication)
    : BaseUploadControllerTests(testApplication, ApiEndpoints.Files );