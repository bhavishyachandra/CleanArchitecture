using CleanArchitecture.Core.Interfaces;
using System.Net.Mail;

namespace CleanArchitecture.Core.Services
{
    public class MessageSender : IMessageSender
    {
        public void SendGuestBookNotificationEmail(string emailAddress, string body)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(emailAddress));
            message.From = new MailAddress("donotreply@guestbook.com");
            message.Subject = "New guestbook entry added";
            message.Body = body;
            using (var client = new SmtpClient("localhost", 25))
            {
                client.Send(message);
            }
        }
    }
}
