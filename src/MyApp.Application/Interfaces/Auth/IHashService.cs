using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.Auth
{
    public interface IHashService
    {
         string GetHash(string input);
    }
}
