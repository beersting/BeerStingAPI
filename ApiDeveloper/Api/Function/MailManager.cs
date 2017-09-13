using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace BeerSting.Api.Function
{
    public class MailManager
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SmtpServer { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }

        public void SendEmail()
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(User);
            mail.To.Add(To);
            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Body = Body;

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = Host;
            SmtpServer.Port = int.Parse(Port);

            SmtpServer.Credentials = new System.Net.NetworkCredential(User, Pass);
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
        }
    }
}
