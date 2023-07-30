using Amazon.S3.Model;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeSystem.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 amazonS3;

        public AwsS3Service(IAmazonS3 amazonS3)
        {
            this.amazonS3 = amazonS3;
        }

        public async Task<string> Upload(Stream stream, string fileName, string bucketName)
        {
            string filePath = $"{Path.GetRandomFileName()}_{fileName}";
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = filePath,
                InputStream = stream
            };
            await amazonS3.PutObjectAsync(putObjectRequest);
            return filePath;
        }

        public async Task Download(string filePath, Stream stream, string bucketName)
        {
            var getObjectRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = filePath
            };
            using var response = await amazonS3.GetObjectAsync(getObjectRequest);
            await response.ResponseStream.CopyToAsync(stream);
        }

        public async Task Delete(string filePath, string bucketName)
        {
            var deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = filePath
            };
            await amazonS3.DeleteObjectAsync(deleteObjectRequest);
        }

    }
}
