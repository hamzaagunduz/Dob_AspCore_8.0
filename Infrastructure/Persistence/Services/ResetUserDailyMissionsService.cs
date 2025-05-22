using Application.Interfaces.IUserDailyMissionRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ResetUserDailyMissionsService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ResetUserDailyMissionsService> _logger;

        public ResetUserDailyMissionsService(IServiceProvider serviceProvider, ILogger<ResetUserDailyMissionsService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextRunTime = DateTime.Today.AddDays(1).AddHours(0); // Gece 12:00
                var delay = nextRunTime - now;

                _logger.LogInformation($"Günlük görev sıfırlama servisi {delay.TotalMinutes:F0} dakika sonra çalışacak.");

                await Task.Delay(delay, stoppingToken); // Gece 12’ye kadar bekle

                using (var scope = _serviceProvider.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetRequiredService<IUserDailyMissionRepository>();
                    await repo.ResetAllUserDailyMissionsAsync();
                    _logger.LogInformation("Tüm kullanıcı görevleri sıfırlandı.");
                }
            }
        }
    }
}
