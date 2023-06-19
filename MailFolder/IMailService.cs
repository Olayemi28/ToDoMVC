using System.Collections.Generic;
using System.Threading.Tasks;
using UniqueTodoApplication.Entities;
using UniqueTodoApplication.Models;

namespace UniqueTodoApplication.MailFolder
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

        Task SendWelcomeEmailAsync(WelcomeRequest request);

        Task Notification(NotificationRequest request, IList<Todoitem> todoitem);

        Task Reminder(ReminderRequest request);

        Task Achievement(AchievementRequest request);

        Task Skipped(SkippedRequest request);
    }
}