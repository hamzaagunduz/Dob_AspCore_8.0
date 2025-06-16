using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Providers
{
    public class AIConfigurationProvider
    {
        private readonly DobContext _context;

        public AIConfigurationProvider(DobContext context)
        {
            _context = context;
        }

        public async Task<OpenAIChatConfig> GetConfigAsync()
        {
            var setting = await _context.AISettings.FirstOrDefaultAsync();

            if (setting == null)
                throw new Exception("AI ayarları bulunamadı.");

            return new OpenAIChatConfig
            {
                ModelId = setting.ModelId,
                ApiKey = setting.ApiKey,
                Endpoint = setting.Endpoint
            };
        }
    }
}