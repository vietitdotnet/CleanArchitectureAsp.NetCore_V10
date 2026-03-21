using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Abstractions.Services
{
    public interface IRequestInfoService
    {
        string? GetIpAddress();
        string? GetUserAgent();
        string? GetDevice();
    }
}
