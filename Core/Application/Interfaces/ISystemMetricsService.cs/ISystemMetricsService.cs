using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ISystemMetricsService.cs
{
    public interface ISystemMetricsService
    {
        Task<SystemMetricsDto> GetSystemMetricsAsync();
    }

    public class SystemMetricsDto
    {
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double DiskUsage { get; set; }
        public double NetworkUsage { get; set; } // Placeholder, detaylı ağ kullanımı ayrı alınabilir
    }
}
