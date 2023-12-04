using LMS_Elibrary.Models;

namespace LMS_Elibrary.Services
{
    public interface IAuthService
    {
        Task<bool> Register(UserDto request);
        Task<string> Login(UserDto request);
    }
}