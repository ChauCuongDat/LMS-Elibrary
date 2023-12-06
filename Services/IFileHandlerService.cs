using LMS_Elibrary.Models;

namespace LMS_Elibrary.Services
{
    public interface IFileHandlerService
    {
        Task<FileForDownload> DownloadFile(string filePath);
        Task<string> SaveAvatar(IFormFile file);
        Task<string> SaveDocument(IFormFile file);
        Task<string> SaveExam(IFormFile file);
        Task<string> SavePriFile(IFormFile file);
    }
}