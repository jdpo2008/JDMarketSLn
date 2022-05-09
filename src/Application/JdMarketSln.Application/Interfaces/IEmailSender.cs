using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdMarketSln.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string from, string to, List<string> copyTo, string subject, string html);
    }
}
