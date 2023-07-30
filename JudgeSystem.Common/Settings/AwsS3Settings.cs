using System;
using System.Collections.Generic;
using System.Text;

namespace JudgeSystem.Common.Settings
{
    public class AwsS3Settings
    {
        public string BucketName { get; set; }
        public string Region { get; set; }
        public string ServiceURL { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }
}
