using LMS_Elibrary.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace LMS_Elibrary.Services
{
    public class FileHandlerService : IFileHandlerService
    {
        public async Task<String> SaveDocument(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No File";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileHandler", "Document", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        public async Task<String> SaveExam(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No File";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileHandler", "Exam", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        public async Task<String> SavePriFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No File";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileHandler", "PriFile", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        public async Task<String> SaveAvatar(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return "No File";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileHandler", "Avatar", file.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return path;
        }

        public async Task<FileForDownload> DownloadFile(string filePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return new FileForDownload(bytes, contentType, Path.GetFileName(filePath));
        }

    }
}
