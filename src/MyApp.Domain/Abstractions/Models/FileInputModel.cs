using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Abstractions.Models
{
    public record class FileInputModel(
    Stream Content,
    string FileName,
    string ContentType,
    long Length);
    
    
}
