using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;

namespace MyApi.Tools
{
    public class Mailer
    {
        public string Send(string receiver, EventArgs e)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(receiver);
                mailMessage.From = new MailAddress("maloba.eric@gmail.com");
                mailMessage.Subject = "ASP.NET e-mail test";
                mailMessage.Body = "Hello world,\n\nThis is an ASP.NET test e-mail!";
                SmtpClient smtpClient = new SmtpClient("smtp.your-isp.com");
                smtpClient.Send(mailMessage);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
