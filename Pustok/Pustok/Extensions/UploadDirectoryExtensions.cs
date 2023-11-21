using Pustok.Contracts;
using Pustok.Exceptions;
using System.IO;

namespace Pustok.Extensions
{
    public static class UploadDirectoryExtensions
    {
        public static string GetAbsolutePath(this UploadDirectory uploadDir)
        {
            switch (uploadDir)
            {
                case UploadDirectory.Products:
                    return @"C:\Users\qarib\Desktop\Code academy\Pustok\Pustok\Pustok\wwwroot\custom-images\products";
                default:
                    throw new UploadDirectoryException("Upload path not found", uploadDir);
            }
        }

        public static string GetAbsolutePath(this UploadDirectory uploadDir, string fileName)
        {
            return Path.Combine(GetAbsolutePath(uploadDir), fileName);
        }
    }
}
