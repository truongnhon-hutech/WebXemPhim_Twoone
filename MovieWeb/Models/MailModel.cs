using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace MovieWeb.Models
{
    public class MailModel
    {
        public string From
        {
            get;
            set;
        }
        public string To
        {
            get;
            set;
        }
        public string Subject
        {
            get;
            set;
        }
        public string Body
        {
            get;
            set;
        }
        public void SendMail()
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(this.To);
            mail.From = new MailAddress(this.From);
            mail.Subject = this.Subject;
            string Body = this.Body;
            mail.Body = Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp-mail.outlook.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("twoonez@outlook.com", "Iknowwhoyouare1001"); // Enter seders User name and password       
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}