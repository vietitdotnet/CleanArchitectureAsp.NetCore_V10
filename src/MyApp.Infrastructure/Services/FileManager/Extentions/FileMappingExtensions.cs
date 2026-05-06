using Microsoft.AspNetCore.Http;
using MyApp.Domain.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Services.FileManager.Extention
{
    public static class FileMappingExtensions
    {
        public static FileInputModel ToInputModel(this IFormFile file)
        {
            return new FileInputModel(
                Content: file.OpenReadStream(),
                FileName: file.FileName,
                ContentType: file.ContentType,
                Length: file.Length
            );
        }

        
    }
}
