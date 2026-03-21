using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Features.Authentications.Requests
{
    public class RefreshTokenRequest
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
