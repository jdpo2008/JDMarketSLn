using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Runtime.InteropServices;
using JDMarketSLn.Application.Common.Interfaces;
using JDMarketSLn.Domain.Settings;
using JDMarketSLn.Application.Interfaces;
using JDMarketSLn.Application.Common.Models;
using JDMarketSLn.Application.Common.Request.Email;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using JDMarketSLn.Application.Common.Exceptions;

namespace JDMarketSLn.Infraestructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        private readonly ILoggerService _logger;

        public EmailService(IOptions<MailSettings> mailSettings, ILoggerService logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task<Result> SendAsync(EmailRequest request, CancellationToken ct = default)
        {
            try
            {
                var email = new MimeMessage();

                #region Sender / Receiver
                // Sender
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.EmailFrom ?? _mailSettings.EmailFrom));
                email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);

                // Reciver
                email.To.Add(MailboxAddress.Parse(request.To));

                if (!string.IsNullOrEmpty(request.ReplyTo))
                    email.ReplyTo.Add(new MailboxAddress(_mailSettings.DisplayName, request.ReplyTo));


                if (request.Copy != null)
                {
                    foreach (string mailAddress in request.Copy.Where(x => !string.IsNullOrWhiteSpace(x)))
                        email.Cc.Add(MailboxAddress.Parse(mailAddress.Trim()));
                }

                #endregion

                #region Content

                var builder = new BodyBuilder();
                email.Subject = request.Subject;
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                if (request.Attachments != null)
                {
                    byte[] attachmentFileByteArray;

                    foreach (IFormFile attachment in request.Attachments)
                    {
                        if (attachment.Length > 0)
                        {
                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                attachment.CopyTo(memoryStream);
                                attachmentFileByteArray = memoryStream.ToArray();
                            }

                            builder.Attachments.Add(attachment.FileName, attachmentFileByteArray, ContentType.Parse(attachment.ContentType));
                        }
                    }
                }


                #endregion


                #region Send Mail
                using var smtp = new SmtpClient();

                if (_mailSettings.UseSSL)
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.SslOnConnect, ct);
                }

                if (_mailSettings.UseStartTls)
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls, ct);
                }

                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                #endregion


                return Result.Success((int)HttpStatusCode.OK);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                throw new ApiException(ex.Message);
            }
        }

        public async Task<Result> SendConfirmationEmailAsync(string toEmail, string fullName, string verificationUri, CancellationToken ct)
        {
            try
            {
                var email = new MimeMessage();

                #region -- Sender / Reciver --

                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.EmailFrom ?? _mailSettings.EmailFrom));
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);

                email.To.Add(MailboxAddress.Parse("jdpo2008@gmail.com"));

                #endregion

                #region -- Content --

                var builder = new BodyBuilder();
                builder.HtmlBody = htmlConfirmationEmail(fullName, verificationUri);
                email.Body = builder.ToMessageBody();
                email.Subject = "InnovacionesJP - Email Confirmation";

                #endregion

                #region -- Send --

                using var smtp = new SmtpClient();

                if (_mailSettings.UseSSL)
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.SslOnConnect, ct);
                }

                if (_mailSettings.UseStartTls)
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls, ct);
                }

                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                #endregion

                return Result.Success(StatusCodes.Status200OK);

            }  
            catch (Exception ex) 
            {
                _logger.LogError(ex, ex.Message.ToString());
                throw new ApiException(ex.Message);
            }

        }

        public async Task<Result> SendForgotPasswordEmailAsync(string toEmail, string fullName, string code, CancellationToken ct = default)
        {
            try
            {
                var email = new MimeMessage();

                #region -- Sender / Reciver --

                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.EmailFrom ?? _mailSettings.EmailFrom));
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);

                email.To.Add(MailboxAddress.Parse(toEmail));

                #endregion

                #region -- Content --

                var builder = new BodyBuilder();
                builder.HtmlBody = HtmlForgotPassword(fullName, code);
                email.Body = builder.ToMessageBody();
                email.Subject = $"Eduka - Reset Password {fullName}";

                #endregion

                #region -- Send --

                using var smtp = new SmtpClient();

                if (_mailSettings.UseSSL)
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.SslOnConnect, ct);
                }

                if (_mailSettings.UseStartTls)
                {
                    await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls, ct);
                }

                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                #endregion

                return Result.Success(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message.ToString());
                throw new ApiException(ex.Message);
            }

        }

        public string htmlConfirmationEmail(string fullname, string verificationUri)
        {
            string html = "";

            html += "<!doctype html>" +
                    "<html" +
                    "<head>" +
                       "<meta charset=\"utf-8\" />" +
                        "<title>forgot password send</title>" +
                    "</head>" +
                    "<body style=\"padding: 50px\">" +
                            $"<h3 style=\"color: darkblue; font-size: 1.1rem\"> Felicitaciones {fullname} </h3>" +

                            "<p style=\"text-align: justify; font-size: 0.9rem\">" +
                                $"Es grato saludarle en informarle que para activar su cuenta debe presionar el siguiente enlace" +
                            "</p>" +

                            $"<a style=\"font-size: 0.9rem; color: blue\" href=\"{verificationUri}\" target=\"_blank\"> Confirmar Cuenta </a>" +

                            "<p style=\"font-size: 0.9rem; font-weight: bolder\">" +
                                "Gracias por preferirnos." +
                            "</p>" +

                            "<p style=\"font-size: 0.9rem; font-style: oblique\">" +
                                "<b>Nota:</b> No responder a este correo." +
                            "</p>" +

                            "<div>" +
                                "<h3 style=\"color: darkblue; font-size: 1.1rem\"> Contáctanos: </h3>" +
                                "<div style=\"display: flex; justify-content: space-between\">" +
                                    "<a style=\"font-size: 0.9rem; color: black; margin-right: 2rem\" href=\"https://www.innovacionesjp.com\" target=\"_blank\"> www.innovacionesjp.com </a>" +
                                    "<span style=\"font-size: 0.9rem\"><b> Teléfono:</b> (+51) 910 380 781 </span>" +
                                "</div>" +
                            "</div>" +
                    "</body" +
                    "</html>";

            return html;

        }

        public string HtmlForgotPassword(string fullname, string code)
        {
            string html = "";

            html += "<!doctype html>" +
                    "<html" +
                    "<head>" +
                       "<meta charset=\"utf-8\" />" +
                        "<title>forgot password send</title>" +
                    "</head>" +
                    "<body style=\"padding: 50px\">" +
                            $"<h3 style=\"color: darkblue; font-size: 1.3rem\"> felicitaciones {fullname} </h3>" +

                            "<p style=\"text-align: justify; font-size: 1rem\">" +
                                $"es grato saludarle en informarle que para recuperar su contraseña use el siguiente codigo: {code}" +
                            "</p>" +

                            "<p style=\"font-size: 1rem; font-weight: bolder\">" +
                                "gracias por preferirnos." +
                            "</p>" +

                            "<p style=\"font-size: 1rem; font-style: oblique\">" +
                                "<b>nota:</b> no responder a este correo." +
                            "</p>" +

                            "<div>" +
                                "<h3 style=\"color: darkblue; font-size: 1.3rem\"> contáctanos: </h3>" +
                                "<div style=\"display: flex; justify-content: space-between\">" +
                                    "<a style=\"font-size: 1rem; color: black; margin-right: 2rem\" href=\"https://www.eduka.com.pe\" target=\"_blank\"> www.eduka.com.pe </a>" +
                                    "<span style=\"font-size: 1rem\"><b> teléfono:</b> (+51) 926 967 676 </span>" +
                                "</div>" +
                            "</div>" +
                    "</body" +
                    "</html>";

            return html;

        }

        //public async Task<Result> SendWelcomeEmailAsync(string toEmail, string userName, CancellationToken ct = default)
        //{
        //    try
        //    {
        //        string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
        //        StreamReader str = new StreamReader(FilePath);
        //        string MailText = str.ReadToEnd();
        //        str.Close();
        //        MailText = MailText.Replace("[username]", userName).Replace("[email]", toEmail);
        //        var email = new MimeMessage();
        //        email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
        //        email.To.Add(MailboxAddress.Parse(toEmail));
        //        email.Subject = $"Welcome {userName}";
        //        var builder = new BodyBuilder();
        //        builder.HtmlBody = MailText;
        //        email.Body = builder.ToMessageBody();
        //        using var smtp = new SmtpClient();
        //        smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
        //        smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
        //        await smtp.SendAsync(email);
        //        smtp.Disconnect(true);

        //        return Result.Success(StatusCodes.Status200OK);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, ex.Message.ToString());
        //        throw new ApiException(ex.Message);
        //    }

        //}
    }
}
