using System.Net;
using System.Net.Mail;

namespace CMS.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task Send(string to, string subject, string body)
        {
            // create message
            var message = new MailMessage();

            message.From = new MailAddress("wshop5323@gmail.com", "Content Management System (CMS)");
            message.Subject = subject;
            message.Body = body;
            message.To.Add(new MailAddress(to));
            message.IsBodyHtml = false;


            try
            {
                var emailClient = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("wshop5323@gmail.com", "undjwamdnreyntbj")
                };


                await emailClient.SendMailAsync(message);
            }
            catch (Exception e)
            {

            }
            // send email

        }
    }
}
