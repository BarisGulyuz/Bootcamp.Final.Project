using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teleperformance.Core.Entities.DTOs.Mail;

namespace Teleperformance.Core.Utilities.Mail
{
    public interface IMailSender
    {
        void Send(MailSenderDto mailSenderDto);
        void Send(MailSenderDto mailSenderDto, string email, string password);
    }
}
