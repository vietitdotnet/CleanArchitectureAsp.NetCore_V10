using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Infrastructure.Services.FileManager.Extentions
{
    public static class FileExtentions
    {
        public static string GetUniqueFileName(string fileName)
        {

            var name = DateTime.Now.ToString("yymmssfff")
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 8)
                      + Path.GetExtension(fileName);

            return name;
        }

        public static string GetUniqueFileWebp()
        {

            var fileName = DateTime.Now.ToString("yymmssfff")
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 8)
                      + ".webp";

            return fileName;
        }
    }
}
