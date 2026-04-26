using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Abstractions
{
    public interface IRoleReference
    {
        string Id { get; }
        string? Name { get; }
    }
}
