using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace CommonLayer.Models
{
    public class SendEmail
    {
        public string EmailSend(string Email, string Token)
        {
            string fromEmail = "npkrish1122@gmail.com";
            MailMessage msg = new MailMessage(fromEmail, Email);
            string resetUrl = $"https://4300/reset-password?token{Token}";
            //string mailBody="Token Generated : "+Token;
            string mailBody = $@"
                    <p>Your Password reset token : <strong>{Token}</strong></p>
                    <p>Click the below link to reset your password: </p>
                    <p><a herf='{resetUrl}'>{resetUrl}</a></p>";
            msg.Subject = "Token generated for the reset password";
            msg.Body = mailBody.ToString();
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential credential = new NetworkCredential("npkrish1122@gmail.com", "gpwp sbro ojzo yesq");

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = credential;
            smtpClient.Send(msg);
            return Email;
        }
    }
}
