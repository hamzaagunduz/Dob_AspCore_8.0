using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Providers
{
    public class IyzipaySettingsProvider
    {
        private readonly DobContext _context;

        public IyzipaySettingsProvider(DobContext context)
        {
            _context = context;
        }

        public async Task<IyzipaySetting> GetSettingsAsync()
        {
            var settings = await _context.IyzipaySettings.FirstOrDefaultAsync();

            if (settings == null)
                throw new Exception("İyzico ayarları bulunamadı.");

            return new IyzipaySetting
            {
                ApiKey = settings.ApiKey,
                SecretKey = settings.SecretKey,
                BaseUrl = settings.BaseUrl,
                CallbackUrl= settings.CallbackUrl,
            };
        }
    }

}
