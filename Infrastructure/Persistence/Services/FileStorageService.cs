using Application.Interfaces.IFileStorageService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public FileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Dosya geçerli değil.");

            // wwwroot klasörünün gerçek yolu
            var webRootPath = _environment.WebRootPath;

            // Kaydedilecek klasörün tam yolu
            var folderPath = Path.Combine(webRootPath, folderName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Dosya adını benzersiz yap
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            // Göreceli dosya yolu (örnek: images/abc123_file.jpg)
            var relativePath = Path.Combine(folderName, uniqueFileName).Replace("\\", "/");

            return relativePath;
        }
    }
}
