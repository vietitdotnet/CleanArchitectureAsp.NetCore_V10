using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.FIleImage
{
    public interface IStorageProvider
    {
        // Lưu một file từ Stream
        Task SaveFileAsync(Stream fileStream, string relativePath);

        // Xóa file
        Task DeleteFileAsync(string relativePath);

        // Kiểm tra file tồn tại
        Task<bool> ExistsAsync(string relativePath);

        // Lấy URL hoặc Path vật lý để truy cập
        string GetFullPath(string relativePath);
    }
}
