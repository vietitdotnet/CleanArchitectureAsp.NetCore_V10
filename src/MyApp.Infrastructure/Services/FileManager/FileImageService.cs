using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using MyApp.Application.Common.Results;
using MyApp.Application.Interfaces.FIleImage;
using MyApp.Domain.Abstractions.Models;
using MyApp.Domain.FileImg;
using MyApp.Infrastructure.Services.FileManager.Extentions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using System;
using System.ClientModel.Primitives;
using static System.Net.Mime.MediaTypeNames;

namespace MyApp.Infrastructure.Services.FileManager
{
    public class FileImageService : IFileImageService
    {
        private readonly IStorageProvider _storageProvider;

        public FileImageService(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }
        
        public async Task<OperationResult<bool>> CreateFileWebpAsync(IObjectFolder objectFolder, FileInputModel file, string? fileName = null)
        {
            try
            {
                // 1. Chuẩn bị đường dẫn lưu trữ (ví dụ: ImageManager/Products/ten-file.webp)
                var webpFileName = fileName ?? FileExtentions.GetUniqueFileWebp();

                 var relativePath = Path.Combine(objectFolder.GetRelativePath(), webpFileName);

                // 2. Xử lý hình ảnh với ImageSharp
                using (var image = await SixLabors.ImageSharp.Image.LoadAsync(file.Content))
                {
                    // Resize ảnh dựa trên cấu hình của ObjectFolder
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(objectFolder.Size, 0)
                    }));

                    // Lưu ảnh đã xử lý vào MemoryStream dưới định dạng WebP
                    using var ms = new MemoryStream();
                    await image.SaveAsync(ms, new WebpEncoder { Quality = objectFolder.Quality });
                    ms.Position = 0; // Quan trọng: Reset con trỏ stream về đầu để Provider có thể đọc được

                    // 3. Đẩy dữ liệu qua StorageProvider để lưu vật lý (Local hoặc Cloud)
                    await _storageProvider.SaveFileAsync(ms, relativePath);
                }

                return OperationResult<bool>.Ok(true, "Tạo và lưu ảnh WebP thành công");
            }
            catch (Exception ex)
            {
                // Ghi log ex ở đây nếu có Logger
                return OperationResult<bool>.Fail($"Lỗi hệ thống khi xử lý ảnh: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> CreateFileOriginAsync(IObjectFolder objectFolder, FileInputModel file, string? fileName = null)
        {
            try
            {
                var path = Path.GetExtension(file.FileName);

                // 1. Chuẩn bị đường dẫn lưu trữ (ví dụ: ImageManager/Products/ten-file.webp)
                var webpFileName = fileName ?? FileExtentions.GetUniqueFileName(path);

                var relativePath = Path.Combine(objectFolder.GetRelativePath(), webpFileName);

                // Lưu trực tiếp không qua xử lý nén
                await _storageProvider.SaveFileAsync(file.Content, relativePath);

                return OperationResult<bool>.Ok(true, "Lưu file gốc thành công");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail($"Lỗi hệ thống khi lưu file gốc: {ex.Message}");
            }
        }

        public async Task<OperationResult<bool>> DeleteFileAsync(IObjectFolder objectFolder, string fileName)
        {
            try
            {

                var relativePath = Path.Combine(objectFolder.GetRelativePath(), fileName);

                if (await _storageProvider.ExistsAsync(relativePath))
                {
                    await _storageProvider.DeleteFileAsync(relativePath);
                    return OperationResult<bool>.Ok(true, "Xóa file thành công");
                }

                return OperationResult<bool>.Fail("File không tồn tại");
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail($"Lỗi khi xóa file: {ex.Message}");
            }
        }
    
    }
}
