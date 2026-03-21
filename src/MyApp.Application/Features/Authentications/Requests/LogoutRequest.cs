using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Authentications.Requests
{
    public class LogoutRequest
    {
        public string RefreshToken { get; set; } = null!;
    }
}
