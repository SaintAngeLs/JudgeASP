using JudgeSystem.Common;
using JudgeSystem.Common.Settings;

using Amazon.S3;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JudgeSystem.Web.Configuration
{
    //public static class AzureBlobStorageConfiguration
    //{
    //    public static IServiceCollection ConfigureAzureBlobStorage(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        AzureBlobSettings azureBlobSettings = configuration.GetSection(AppSettingsSections.AzureBlobSection).Get<AzureBlobSettings>();

    //        //If someone try to start the application but have no azure storage account, just will skip adding azure storage related services to the DI container
    //        if (azureBlobSettings == null)
    //        {
    //            return services;
    //        }

    //        var storageAccount = CloudStorageAccount.Parse(azureBlobSettings.StorageConnectionString);
    //        services.AddSingleton(storageAccount);

    //        CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

    //        CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(azureBlobSettings.ContainerName);
    //        services.AddSingleton(cloudBlobContainer);

    //        return services;
    //    }
    //}
    //public static class AwsS3Configuration
    //{
    //    public static IServiceCollection ConfigureAwsS3(this IServiceCollection services, IConfiguration configuration)
    //    {
    //        AwsS3Settings awsS3Settings = configuration.GetSection(AppSettingsSections.AwsS3Section).Get<AwsS3Settings>();

    //        // If someone tries to start the application but has no AWS S3 storage account, just skip adding AWS S3 related services to the DI container
    //        if (awsS3Settings == null)
    //        {
    //            return services;
    //        }

    //        AmazonS3Config s3Config = new AmazonS3Config
    //        {
    //            RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsS3Settings.Region),
    //            ServiceURL = awsS3Settings.ServiceURL,
    //        };
    //        services.AddSingleton(s3Config);

    //        AmazonS3Client s3Client = new AmazonS3Client(awsS3Settings.AccessKey, awsS3Settings.SecretKey, s3Config);
    //        services.AddSingleton(s3Client);

    //        return services;
    //    }
    //}
}
