using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeSystem.Services
{
    public interface IAwsS3Service
    {
        Task<string> Upload(Stream stream, string inputFileName, string bucketName);

        Task Download(string filePath, Stream stream, string bucketName);

        Task Delete(string filePath, string bucketName);
    }
}
