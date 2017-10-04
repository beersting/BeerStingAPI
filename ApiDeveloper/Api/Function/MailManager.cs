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
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachments = new List<string>();
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
            mail.CC.Add(Cc);
            mail.Subject = Subject;
            mail.IsBodyHtml = true;
            mail.Body = Body;

            for (int i = 0; i < Attachments.Count; i++)
            {
                mail.Attachments.Add(new Attachment(Attachments[i]));
            }

            SmtpClient SmtpServer = new SmtpClient();
            SmtpServer.Host = Host;
            SmtpServer.Port = int.Parse(Port);
            if (!String.IsNullOrEmpty(Pass))
            {
                SmtpServer.Credentials = new System.Net.NetworkCredential(User, Pass);
                SmtpServer.EnableSsl = true;
            }
            SmtpServer.Send(mail);
        }
    }
}
