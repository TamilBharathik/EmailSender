using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;

namespace TestMail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendMail(string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("sender@gmail.com"));
            email.To.Add(MailboxAddress.Parse("receiver@gmail.com"));

            email.Subject = "TestingMail";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("xxx@gmail.com", "--ReplaceToAppPassword--");
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok();
        }
    }
}
