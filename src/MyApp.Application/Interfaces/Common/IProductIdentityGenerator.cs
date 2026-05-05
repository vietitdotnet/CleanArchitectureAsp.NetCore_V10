using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Application.Interfaces.Common
{
    public interface IProductIdentityGenerator
    {

        string GenerateSlug(string input);

        string GenerateSku(string? inputSku = null, string prefix = "PRD");
    }
}
