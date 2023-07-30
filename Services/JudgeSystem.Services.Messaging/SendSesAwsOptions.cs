using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudgeSystem.Services.Messaging
{
    public class SendSesAwsOptions
    {
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Region { get; set; }
    }
}
