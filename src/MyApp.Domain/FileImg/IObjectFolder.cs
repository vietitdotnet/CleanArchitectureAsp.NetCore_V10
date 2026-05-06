using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.FileImg
{
    public interface IObjectFolder
    {
        string FolderName { get; }
        int Size { get; }
        int Quality { get; }
        string GetRelativePath();
    }
}
