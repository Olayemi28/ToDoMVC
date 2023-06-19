using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using UniqueTodoApplication.Context;
using UniqueTodoApplication.Interface.IRepositries;
using UniqueTodoApplication.Interface.IService;

namespace UniqueTodoApplication
{
    public class UpcomingTask : BackgroundService
    {

        private readonly SkippedMailsConfig _configuration;
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly ILogger<UpcomingTask> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public UpcomingTask(IServiceScopeFactory serviceScopeFactory, IOptions<SkippedMailsConfig> configuration, ILogger<UpcomingTask> logger)
        {
             _configuration = configuration.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _schedule = CrontabSchedule.Parse(_configuration.CronExpression);
            _nextRun = _schedule.GetNextOccurrence(DateTime.UtcNow);
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.UtcNow;
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                    var todoitemContext = scope.ServiceProvider.GetRequiredService<ITodoitemRepository>();
                    var reminderContext = scope.ServiceProvider.GetRequiredService<IReminderRepository>();
                    var customerContext = scope.ServiceProvider.GetRequiredService<ICustomerService>();
                    var task = await todoitemContext.GetAllTBUUpcomingTask();
                    foreach (var item in task)
                    {
                        item.Status = Enum.Status.Today;                        
                    }
                    context.UpdateRange(task);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error occured reading Reminder Table in database. {ex.Message}");
                    _logger.LogError(ex, ex.Message);
                }
                _logger.LogInformation($"Background Hosted Service for {nameof(UpcomingTask)} is stopping ");
                var timeSpan = _nextRun - now;
                await Task.Delay(timeSpan, stoppingToken);
                
            }   
        }
    }
}