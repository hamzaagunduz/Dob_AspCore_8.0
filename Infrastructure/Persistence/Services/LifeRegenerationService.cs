using Application.Interfaces.IUserRepository;
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
    public class LifeRegenerationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<LifeRegenerationService> _logger;

        public LifeRegenerationService(IServiceProvider serviceProvider, ILogger<LifeRegenerationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

                var users = await userRepository.GetAllAsync();

                foreach (var user in users)
                {
                    if (user.Lives >= 10)
                        continue;

                    var now = DateTime.UtcNow;
                    var last = user.LastLifeAddedTime ?? now;
                    var minutesPassed = (now - last).TotalMinutes;
                    int livesToAdd = (int)(minutesPassed / 20);

                    if (livesToAdd > 0)
                    {
                        user.Lives = Math.Min(user.Lives + livesToAdd, 10);
                        user.LastLifeAddedTime = last.AddMinutes(livesToAdd * 20);
                        await userRepository.UpdateAsync(user);
                    }
                }

                _logger.LogInformation("Can güncellemesi tamamlandı: {time}", DateTime.UtcNow);

                await Task.Delay(TimeSpan.FromMinutes(20), stoppingToken); // 5 dakikada bir çalıştır
            }
        }
    }

}
