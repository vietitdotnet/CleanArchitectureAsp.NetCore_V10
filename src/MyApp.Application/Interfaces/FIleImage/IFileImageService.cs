using MyApp.Application.Common.Results;
using MyApp.Domain.Abstractions.Models;
using MyApp.Domain.FileImg;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.FIleImage
{
    public interface IFileImageService
    {
        Task<OperationResult<bool>> CreateFileWebpAsync(IObjectFolder objectFolder, FileInputModel file, string? fileName = null);

        Task<OperationResult<bool>> CreateFileOriginAsync(IObjectFolder objectFolder, FileInputModel file, string? fileName = null);

        Task<OperationResult<bool>> DeleteFileAsync(IObjectFolder objectFolder, string fileName);

    }
}
