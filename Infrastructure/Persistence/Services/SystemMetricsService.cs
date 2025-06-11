using Application.Interfaces;
using Application.Interfaces.ISystemMetricsService.cs;
using Hardware.Info;
using System.Diagnostics;
using System.IO;

namespace Infrastructure.Persistence.Services
{
    public class SystemMetricsService : ISystemMetricsService
    {
        private readonly IHardwareInfo _hardwareInfo;

        public SystemMetricsService()
        {
            _hardwareInfo = new HardwareInfo();
        }

        public async Task<SystemMetricsDto> GetSystemMetricsAsync()
        {
            _hardwareInfo.RefreshMemoryStatus();

            // CPU
            float cpuUsage = GetCpuUsage();

            // Bellek
            var memory = _hardwareInfo.MemoryStatus;
            var memoryUsage = 100 - (memory.AvailablePhysical / (double)memory.TotalPhysical * 100);

            // Disk (C:\ sürücüsünü baz alıyoruz)
            var drive = DriveInfo.GetDrives().FirstOrDefault(d => d.IsReady && d.Name == "C:\\");
            double diskUsage = 0;
            if (drive != null)
            {
                double totalSize = drive.TotalSize;
                double freeSpace = drive.TotalFreeSpace;
                diskUsage = 100 - (freeSpace / totalSize * 100);
            }

            // Ağ (placeholder)
            double networkUsage = 0.0;

            return new SystemMetricsDto
            {
                CpuUsage = Math.Round(cpuUsage, 2),
                MemoryUsage = Math.Round(memoryUsage, 2),
                DiskUsage = Math.Round(diskUsage, 2),
                NetworkUsage = networkUsage
            };
        }

        private float GetCpuUsage()
        {
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            cpuCounter.NextValue();
            Thread.Sleep(1000);
            return cpuCounter.NextValue();
        }
    }
}
