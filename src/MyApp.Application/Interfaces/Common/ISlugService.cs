using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.Common
{
    public interface ISlugService
    {
        string Generate(string input);
    }
}
