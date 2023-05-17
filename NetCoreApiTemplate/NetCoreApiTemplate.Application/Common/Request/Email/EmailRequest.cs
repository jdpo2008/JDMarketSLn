using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Common.Request.Email
{
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Copy { get; set; }
        public string From { get; set; }
        public string ReplyTo { get; set; }
        public List<IFormFile> Attachments { get; set; }


        public EmailRequest() { }

        public EmailRequest(string To, string Subject, string body = null, List<string> Copy = null, string From = null, string ReplyTo = null, List<IFormFile> Attachments = null)
        {
            this.To = To;
            this.Subject = Subject;
            this.Body = body;
            this.Copy = Copy ?? new List<string>();
            this.From = From;
            this.ReplyTo = ReplyTo;
            this.Attachments = Attachments ?? new List<IFormFile>();

        }
    }
}
