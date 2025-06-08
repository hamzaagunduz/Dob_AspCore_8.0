using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IFileStorageService
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName, CancellationToken cancellationToken);

        Task DeleteFileAsync(string filePath); // Add this method

    }
}
