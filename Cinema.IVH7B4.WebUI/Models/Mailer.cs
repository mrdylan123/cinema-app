using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Cinema.IVH7B4.Domain.Entities;

namespace Cinema.IVH7B4.WebUI.Models
{
    public class Mailer
    {

        public void SendMail(List<NewsletterRegistration> emails, string mailtext, string subject/*, string pathtoattachment*/)
        {
            // Set up mail credentials
            MailAddress mailFrom = new MailAddress("Biosboopb4@gmail.com");

            // Configure the SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;   // 465 might also work, in case 587 wont
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("bioscoopb4@gmail.com", "avansb4!");
            smtp.Host = "smtp.gmail.com";

            // Create the mail for each recipient and send it
            for (int i = 0; i < emails.Count; i++)
            {
                NewsletterRegistration m = emails[i];

                MailAddress mailTo = new MailAddress(m.Email);
                MailMessage mm = new MailMessage(mailFrom, mailTo);
                mm.Body = "Beste " + m.Name + ",<br /><br />" + mailtext +
                          "<br /><br />Met vriendelijke groet, <br/> Avans Bioscopen";
                mm.IsBodyHtml = true;
                mm.Subject = subject;
                //mm.Attachments.Add(new Attachment(pathtoattachment));
                smtp.Send(mm);
            }

        }
    }
}