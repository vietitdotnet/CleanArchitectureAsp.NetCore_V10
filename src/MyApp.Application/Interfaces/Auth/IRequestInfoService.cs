using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.Auth
{
    public interface IRequestInfoService
    {
        string? GetIpAddress();
        string? GetUserAgent();
        string? GetDevice();
    }
}
