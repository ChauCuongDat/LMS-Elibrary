using LMS_Elibrary.Models;
using LMS_Elibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemController : ControllerBase
    {
        private readonly ICRUDService _crudService;
        private readonly IFileHandlerService _fileHandlerService;
        public SystemController(ICRUDService CRUDService, IFileHandlerService fileHandlerService)
        {
            _crudService = CRUDService;
            _fileHandlerService = fileHandlerService;
        }

        //Function 4: Manage notification

        [HttpGet("1 Get all notificaton")]
        public List<Notification> GetNotifications(string userId)
        {
            return _crudService.getNotisByUser(userId);
        }

        [HttpGet("2 Search notification")]
        public List<Notification> searchNotification(string searchS, string userId)
        {
            return _crudService.searchNoti(searchS, userId);
        }

        [HttpDelete("3 Delete notification")]
        public string deleteNotification(int notiId)
        {
            return _crudService.removeNotification(notiId);
        }

        [HttpPatch("4 Mark notification as not-read")]
        public string unreadNoti(int notiId)
        {
            return _crudService.unreadNoti(notiId);
        }

        [HttpPatch("5 Mark notification as read")]
        public string readNoti(int notiId)
        {
            return _crudService.readNoti(notiId);
        }

        [HttpGet("7 Open setting")]
        public Setting openSetting (string userId)
        {
            return _crudService.getSetting(userId);
        }

        [HttpPatch("7.2 Turn on/off notification")]
        public string changeNotificationStatus(string userId, bool status)
        {
            return _crudService.changeNotification(status, userId);
        }

        //Function 6: Help

        [HttpGet("1 See help")]
        public List<Help> seeHelp()
        {
            return _crudService.getHelps();
        }

        [HttpPost("2 Ask For Help")]
        public string askForHelp(Help help)
        {
            return _crudService.addHelp(help);
        }

        //Function 7: Manage Account

        [HttpGet("1 Get account detail")]
        public UserDto getUser(string userId)
        {
            return _crudService.GetUser(userId);
        }

        [HttpPatch("1.1 Change avatar")]
        public async Task<string> changeAvatar(string userId, IFormFile avatar)
        {
            return await _crudService.changeAvatar(avatar, userId);
        }

        [HttpPatch("1.1.2 Delete avatar")]
        public string deleteAvatar(string userId)
        {
            UserDto user = _crudService.GetUser(userId);
            _fileHandlerService.DeleteFile(user.Avatar);
            user.Avatar = null;
            _crudService.updateUser(user);
            return "Avatar deleted";
        }

        [HttpPatch("2 Change password")]
        public async Task<IdentityResult> changePassword(string userId, string oldPassword, string newPassword)
        {
            return await _crudService.changePassword(userId, oldPassword, newPassword);
        }
    }
}
