using NetCoreApiTemplate.Application.Common.Models;
using NetCoreApiTemplate.Application.Common.Request.Email;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCoreApiTemplate.Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendAsync(EmailRequest request, CancellationToken ct);
        Task<Result> SendConfirmationEmailAsync(string toEmail, string fullName, string verificationUri, CancellationToken ct);
        Task<Result> SendForgotPasswordEmailAsync(string toEmail, string fullName, string code, CancellationToken ct);
        //Task<Response<string>> SendWelcomeEmailAsync(string toEmail, string userName, CancellationToken ct);
        //Task<Response<string>> SendInformationEmailAsync(string toEmail, string fullName, string curso, CancellationToken ct);
    }
}
