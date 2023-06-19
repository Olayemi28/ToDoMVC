using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace UniqueTodoApplication.BackgroundTask
{
    public class AchievementMails : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new System.NotImplementedException();
        }
    }
}