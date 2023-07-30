
using JudgeSystem.Common;
using JudgeSystem.Common.Settings;

using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace JudgeSystem.Web.Configuration
{
    
    public static class AwsS3Configuration
    {
        public static IServiceCollection ConfigureAwsS3(this IServiceCollection services, IConfiguration configuration)
        {
            AwsS3Settings awsS3Settings = configuration.GetSection("AwsS3").Get<AwsS3Settings>(); // make sure to get the correct section
            Console.WriteLine($"Region: {awsS3Settings.Region}");
            Console.WriteLine($"BucketName: {awsS3Settings.BucketName}");
            Console.WriteLine($"AccessKey: {awsS3Settings.AccessKey}");
            Console.WriteLine($"SecretKey: {awsS3Settings.SecretKey}");
            // If someone tries to start the application but has no AWS S3 storage account, just skip adding AWS S3 related services to the DI container
            if (awsS3Settings == null)
            {
                return services;
            }

            AmazonS3Client s3Client = new AmazonS3Client(awsS3Settings.AccessKey, awsS3Settings.SecretKey, Amazon.RegionEndpoint.GetBySystemName(awsS3Settings.Region));
            services.AddSingleton<IAmazonS3>(s3Client);

            return services;
        }
    }
    
}
