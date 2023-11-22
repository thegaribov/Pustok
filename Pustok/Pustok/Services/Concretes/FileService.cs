using Microsoft.AspNetCore.Http;
using Pustok.Contracts;
using Pustok.Extensions;
using Pustok.Services.Abstract;
using System;
using System.IO;
using System.Net.Sockets;

namespace Pustok.Services.Concretes
{
    public class FileService : IFileService
    {
        public string Upload(IFormFile file, string path)
        {
            var uniqueFileName = GetUniqueFileName(file.FileName);
            var uploadPath = Path.Combine(path, uniqueFileName);
            using FileStream fileStream = new FileStream(uploadPath, FileMode.Create);
            file.CopyTo(fileStream);

            return uniqueFileName;
        }
      
        public string Upload(IFormFile file, UploadDirectory uploadDir)
        {
            var uniqueFileName = GetUniqueFileName(file.FileName);
            var uploadPath = uploadDir.GetAbsolutePath(uniqueFileName);
            using FileStream fileStream = new FileStream(uploadPath, FileMode.Create);
            file.CopyTo(fileStream);

            return uniqueFileName;
        } 

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public void Delete(UploadDirectory uploadDir, string fileName)
        {
            var absolutePath = uploadDir.GetAbsolutePath(fileName);

            Delete(absolutePath);
        }

        private string GetUniqueFileName(string originalFileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        }
    }
}
