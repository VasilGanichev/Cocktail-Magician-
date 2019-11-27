using CocktailMagician.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class MailServices : IMailServices
    {
        private const string APP_EMAIL = "cocktailmagicianteam@gmail.com";
        private const string EMAIL_PASS = "CocktailMagician123";
        private readonly IFormattingService formattingService;

        public MailServices(IFormattingService formattingService)
        {

            this.formattingService = formattingService;
        }

        public async Task<bool> SendEmailToGroup(string from, string[] to, string subject, string body, List<byte[]> attachments = null)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(APP_EMAIL);
            to.ToList().ForEach(x => mail.To.Add(x));
            mail.Subject = subject;
            mail.Body = body;

            foreach (var attachment in attachments)
            {
                var memStream = new MemoryStream(attachment);
                memStream.Position = 0;
                var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                var reportAttachment = new Attachment(memStream, contentType);
                reportAttachment.ContentDisposition.FileName = "yourFileName.pdf";
                mail.Attachments.Add(reportAttachment);
            }


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(APP_EMAIL, EMAIL_PASS);
            SmtpServer.EnableSsl = true;

            try
            {
                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public async Task<bool> SendEmailToGroup(string from, string[] to, string subject, string body, byte[] attachment = null)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(APP_EMAIL);
            to.ToList().ForEach(x => mail.To.Add(x));
            mail.Subject = subject;
            mail.Body = body;


            var memStream = new MemoryStream(attachment);
            memStream.Position = 0;
            var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
            var reportAttachment = new Attachment(memStream, contentType);
            reportAttachment.ContentDisposition.FileName = "yourFileName.pdf";
            mail.Attachments.Add(reportAttachment);


            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(APP_EMAIL, EMAIL_PASS);
            SmtpServer.EnableSsl = true;

            try
            {
                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

    }
}
