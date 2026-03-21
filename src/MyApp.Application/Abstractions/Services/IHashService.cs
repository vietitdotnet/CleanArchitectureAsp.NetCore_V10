using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Abstractions.Services
{
    public interface IHashService
    {
         string GetHash(string input);
    }
}
