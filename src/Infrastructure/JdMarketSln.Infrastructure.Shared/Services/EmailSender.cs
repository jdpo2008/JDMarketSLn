using JdMarketSln.Application.Interfaces;
using JdMarketSln.Domain.Settings;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using JdMarketSln.Application.Exceptions;

namespace JdMarketSln.Infrastructure.Shared.Services
{
    public class EmailSender : IEmailSender
    {
        public MailSettings _mailSettings { get; }

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }


        public async Task SendEmailAsync(string from, string to, List<string> copyTo, string subject, string html)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(from ?? _mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(to));

                // CC Message
                if(copyTo.Count > 0)
                {
                    foreach (var item in copyTo)
                    {
                        email.Cc.Add(MailboxAddress.Parse(item));
                    }
                }
                    
                email.Subject = subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = html;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (System.Exception ex)
            {
                throw new ApiException(ex.Message);
            }
        }
    }
}
