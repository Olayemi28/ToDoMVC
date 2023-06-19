using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Interface.IService;
using UniqueTodoApplication.Models;
using UniqueTodoApplication.Settings;


namespace UniqueTodoApplication.MailFolder
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ITodoitemService _todoitemService;

        public MailService(IOptions<MailSettings> mailSettings,ITodoitemService todoitemService)
        {
            _mailSettings = mailSettings.Value;
            _todoitemService = todoitemService;
        }

        public async Task Achievement(AchievementRequest request)
        {
          string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", $"{request.FirstName} ").Replace("[name]", request.Name).Replace("[time]",request.OriginalTime.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
           email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Task Reminder";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

        }

        
        public async Task Notification(NotificationRequest request, IList<Todoitem> todoitem)
        {
           StringBuilder builder = new StringBuilder();
           
            builder.Append("<p style='color: #000000; background-color: #ffffff'>Below are the Users registered on Unique Todo System. <br> </ p>");
            builder.Append("<table border=1 ><tr>");          

            builder.Append("<table border=1 ><tr>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "Name" + "</th>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "OriginalTime" + "</th>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "TimeInterval" + "</th>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "Description" + "</th>");
            builder.Append("<th style='font-family: Arial; font-size: 10pt;'>" + "Priority" + "</th>");
            builder.Append("</tr>");

                foreach (var data in todoitem)
                {
                    builder.Append("<tr>");
                    builder.Append("<td>" + data.Name + "</td>");
                    builder.Append("<td>" + data.OriginalTime + "</td>");
                    builder.Append("<td>" + data.TimeInterval + "</td>");
                    builder.Append("<td>" + data.Description + "</td>");
                    builder.Append("<td>" + data.Priority + "</td>");
                    builder.Append("</tr>");
                }

                builder.Append("</table>");

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = request.Subject;

             var mainBuilder = new BodyBuilder();
            mainBuilder.HtmlBody = builder.ToString();
            email.Body = mainBuilder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task Reminder(ReminderRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\ReminderTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", $"{request.FirstName} ").Replace("[name]", request.Name).Replace("[time]",request.OriginalTime.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Task Reminder";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(WelcomeRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", $"{request.FirstName} {request.LastName}").Replace("[email]", request.Email);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = $"Dear {request.FirstName} {request.LastName}, you are highly welcome to our application.";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task Skipped(SkippedRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\SkippedTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", $"{request.FirstName} ").Replace("[name]", request.Name).Replace("[time]",request.OriginalTime.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
           email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Task Reminder";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}