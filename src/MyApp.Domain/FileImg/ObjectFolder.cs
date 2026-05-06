using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.FileImg
{
    public abstract class ObjectFolder : IObjectFolder
    {
        // Để default giá trị tại đây hoặc thông qua Constructor
        public virtual int Size { get; protected set; } = 400;
        public virtual int Quality { get; protected set; } = 75;
        public abstract string FolderName { get; }

        public string GetRelativePath()
        {
            // Trả về: ImageManager/Product
            return Path.Combine(PathForder.RootDirectory, FolderName);
        }
    }
}
