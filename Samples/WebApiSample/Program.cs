using Amazon;
using Amazon.S3;
using ManagedCode.Storage.Aws.Extensions;
using ManagedCode.Storage.Azure.Extensions;
using ManagedCode.Storage.Azure.Options;
using ManagedCode.Storage.FileSystem.Extensions;
using ManagedCode.Storage.FileSystem.Options;
using ManagedCode.Storage.Google.Extensions;
using ManagedCode.Storage.Google.Options;
using WebApiSample.Interfaces;
using WebApiSample.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();


#region Add FileSystemStorage

builder.Services.AddFileSystemStorageAsDefault(new FileSystemStorageOptions
{
    BaseFolder = Path.Combine(Environment.CurrentDirectory, "managed-code-bucket")
});

builder.Services.AddFileSystemStorage(new FileSystemStorageOptions
{
    BaseFolder = Path.Combine(Environment.CurrentDirectory, "managed-code-bucket")
});

#endregion

#region Add AzureStorage

builder.Services.AddAzureStorage(new AzureStorageOptions
{
    Container = "container-name",
    ConnectionString =
        "connection-string"
});

#endregion

#region Add AWSSorage

// AWS library overwrites property values. you should only create configurations this way. 
var awsConfig = new AmazonS3Config
{
    RegionEndpoint = RegionEndpoint.EUWest1,
    ForcePathStyle = true,
    UseHttp = true,
    ServiceURL = "http://localhost:4566" // this is the default port for the aws s3 emulator, must be last in the list
};

builder.Services.AddAWSStorage(opt =>
{
    opt.PublicKey = "localkey";
    opt.SecretKey = "localsecret";
    opt.Bucket = "managed-code-bucket";
    opt.OriginalOptions = awsConfig;
});

#endregion

#region Add GCPStorage

// builder.Services.AddGCPStorage(new GCPStorageOptions
// {
//     BucketOptions = new BucketOptions
//     {
//         ProjectId = "api-project-0000000000000",
//         Bucket = "managed-code-bucket"
//     }
// });

#endregion

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();