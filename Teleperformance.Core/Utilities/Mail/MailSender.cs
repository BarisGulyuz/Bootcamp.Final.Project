using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs.Mail;

namespace Teleperformance.Core.Utilities.Mail
{
  public  class MailSender : IMailSender
    {
        private readonly SmtpSettings _smtpSettings;
        public MailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public void Send(MailSenderDto mailSenderDto)
        {
            MailMessage mailMessage = new()
            {
                From = new MailAddress(_smtpSettings.SenderEmail),
                To = { new MailAddress(mailSenderDto.Email) },
                Subject = mailSenderDto.Subject,
                IsBodyHtml = true,
                Body = mailSenderDto.Message
            };
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.UserName, _smtpSettings.Password)
            };

            smtpClient.Send(mailMessage);
        }

        public void Send(MailSenderDto mailSenderDto, string email, string password)
        {
            MailMessage mailMessage = new()
            {
                From = new MailAddress(email),
                To = { new MailAddress(mailSenderDto.Email) },
                Subject = mailSenderDto.Subject,
                IsBodyHtml = true,
                Body = mailSenderDto.Message
            };
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(email, password)
            };

            smtpClient.Send(mailMessage);
        }
    }
}
