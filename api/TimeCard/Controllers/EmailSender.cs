using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Controllers
{

    public class EmailSender : BaseController
    {
        private readonly IEmailSend _emailSend;

        public EmailSender(IEmailSend emailSend)
        {
            _emailSend = emailSend;
        }

        [HttpPost]
        public IActionResult GetEmailResponse(MailDto sendersEmail)
        {
            var message = new MessageClass(new string[] { sendersEmail.UserEmail }, "Welcome Email", "This is the content from our email.");
            _emailSend.SendEmail(message);
            return Ok();
        }
    }
}