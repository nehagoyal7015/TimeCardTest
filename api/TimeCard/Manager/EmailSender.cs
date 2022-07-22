using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class EmailSender : IEmailSend
    {
        private readonly EmailConfig _emailConfig;
        public EmailSender(EmailConfig emailConfig)
        {
            _emailConfig = emailConfig;
        }
        public void SendEmail(MessageClass message)
        {
            var emailMessage = CreateEmailMessage(message);
            
            Send(emailMessage);
        }

        private MimeMessage CreateEmailMessage(MessageClass message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "<h1> welcomeeeeeeeeee</h1>" };
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.UserName, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}