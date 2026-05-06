using Microsoft.AspNetCore.Hosting;
using MyApp.Application.Interfaces.FIleImage;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Services.FileManager
{
    public class LocalStorageProvider : IStorageProvider
    {
        private readonly IWebHostEnvironment _webHost;

        public LocalStorageProvider(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task SaveFileAsync(Stream fileStream, string relativePath)
        {
            var fullPath = Path.Combine(_webHost.WebRootPath, relativePath);
            var directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);

            using var targetStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
            await fileStream.CopyToAsync(targetStream);
        }

        public Task DeleteFileAsync(string relativePath)
        {
            var fullPath = Path.Combine(_webHost.WebRootPath, relativePath);
            if (File.Exists(fullPath)) File.Delete(fullPath);
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(string relativePath)
        {
            var fullPath = Path.Combine(_webHost.WebRootPath, relativePath);
            return Task.FromResult(File.Exists(fullPath));
        }

        public string GetFullPath(string relativePath)
        {
            // Trả về path dạng /ImageManager/Product/image.webp để hiển thị trên web
            return "/" + relativePath.Replace("\\", "/");
        }
    }
}
