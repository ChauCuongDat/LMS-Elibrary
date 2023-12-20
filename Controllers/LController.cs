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
    [Authorize(Roles = "Admin,Leadership")]
    public class LController : ControllerBase
    {
        private readonly ICRUDService _crudService;
        private readonly IFileHandlerService _fileHandlerService;
        public LController(ICRUDService CRUDService, IFileHandlerService fileHandlerService)
        {
            _crudService = CRUDService;
            _fileHandlerService = fileHandlerService;
        }

        //Function 1: Subject management

        [HttpGet("1 Get subject list")]
        public List<Subject> GetSubjects()
        {
            return _crudService.GetSubjects();
        }

        [HttpGet("1.2 Get subject documents")]
        public List<Document> GetDocumentsBySubject(int subjectId)
        {
            return _crudService.getDocBySub(subjectId);
        }

        [HttpPatch("1.2.1 Approve document")]
        public string approveDocument(int docId, string approverId)
        {
            return _crudService.approveDoc(docId, approverId);
        }

        [HttpGet("1.2.2 Download document")]
        public async Task<IActionResult> downloadDocument(int docId)
        {
            string filePath = _crudService.getDoc(docId).FileAddress;
            FileForDownload file = await _fileHandlerService.DownloadFile(filePath);
            return File(file.bytes,file.contentType,file.fileName);
        }

        [HttpPatch("1.2.3 Unapprove document")]
        public string unapproveDocument(int docId, string approverId)
        {
            return _crudService.disapprDoc(docId, approverId);
        }

        [HttpGet("1.2.5 Filter document by status")]
        public List<Document> filterDocByStatus(bool? status)
        {
            return _crudService.getDocByStatus(status);
        }

        [HttpGet("1.3 Get subject detail")]
        public Subject GetSubject(int subjectId)
        {
            return _crudService.GetSubject(subjectId);
        }

        [HttpGet("1.3 Get subject detail - topic list")]
        public List<Topic> getTopics (int subjectId)
        {
            return _crudService.GetSubTopics(subjectId);
        }

        //Function 2: Private file management

        [HttpGet("1 Get all user's private file")]
        public List<PrivateFile> GetPrivateFiles(string userId)
        {
            return _crudService.getPriFiles(userId);
        }

        [HttpPost("2 Upload private file")]
        public string uploadPrivateFile (PrivateFile priFile, IFormFile file)
        {
            return _crudService.addPriFile(priFile, file);
        }

        [HttpGet("3 Download private file")]
        public async Task<IActionResult> downloadPrivateFile(int fileId)
        {
            string filePath = _crudService.getPrivateFile(fileId).FileAddress;
            FileForDownload file = await _fileHandlerService.DownloadFile(filePath);
            return File(file.bytes, file.contentType, file.fileName);
        }

        [HttpGet("4 Search for private file")]
        public List<PrivateFile> searchPrivateFile (string searchS)
        {
            return _crudService.searchPrifile(searchS);
        }

        [HttpGet("5 Filter private file by type")]
        public List<PrivateFile> filterPrivateFileByType(string type)
        {
            return _crudService.getPriFilesByTypes(type);
        }

        [HttpPatch("7 Change private file name")]
        public string changePrivateFileName(int fileId, string name)
        {
            return _crudService.changePriFileName(fileId, name);
        }

        [HttpDelete("8 Delete private file")]
        public string deletePrivateFile(int fileId)
        {
            return _crudService.removePriFile(fileId);
        }

        //Function 3: Exam management

        [HttpGet("1 Get all exam")]
        public List<Exam> getExamlist()
        {
            return _crudService.getExamList();
        }

        [HttpGet("2 Filter exam - by subject")]
        public List<Exam> filterExamBySubject(int subId)
        {
            return _crudService.getExamsBySub(subId);
        }

        [HttpGet("2 Filter exam - by teacher")]
        public List<Exam> filterExamByTeacher(string userId)
        {
            return _crudService.getExamsByTeach(userId);
        }

        [HttpGet("2 Filter exam - by status")]
        public List<Exam> filterExamByStatus(bool? status)
        {
            return _crudService.getExamsByStatus(status);
        }

        [HttpPatch("3 Approve exam")]
        public string approveExam(int examId)
        {
            return _crudService.approExam(examId);
        }

        [HttpPatch("4 Unapprove exam")]
        public string unapproveExam(int examId)
        {
            return _crudService.disapproExam(examId);
        }

        [HttpGet("5 Get Exam detail")]
        public Exam GetExam(int examId)
        {
            return _crudService.getExam(examId);
        }

        [HttpGet("6 Search exam")]
        public List<Exam> searchExam(string searchS)
        {
            return _crudService.searchExam(searchS);
        }

        [HttpGet("7 Download exam")]
        public async Task<IActionResult> downloadExam(int examId)
        {
            string filePath = _crudService.getExam(examId).FileAdddress;
            FileForDownload file = await _fileHandlerService.DownloadFile(filePath);
            return File(file.bytes, file.contentType, file.fileName);
        }

        //Function 5: Setting

        [HttpGet("1 See setting")]
        public Setting GetSetting(string userId)
        {
            return _crudService.getSetting(userId);
        }

        [HttpPatch("1.1 Change setting")]
        public string changeSetting(Setting setting)
        {
            return _crudService.changeSetting(setting);
        }

        [HttpGet("2 Get role list")]
        public List<IdentityRole> getRoleList()
        {
            return _crudService.GetRoles();
        }

        [HttpPost("2.1 Add role")]
        public string addRole(IdentityRole role)
        {
            return _crudService.addRole(role);
        }

        [HttpPatch("2.2 Edit role")]
        public string editRole(IdentityRole role)
        {
            return _crudService.updateRole(role);
        }

        [HttpDelete("2.3 Remove role")]
        public string deleteRole(IdentityRole role)
        {
            return _crudService.removeRole(role);
        }

        [HttpGet("3 Get user list")]
        public List<UserDto> getUserList()
        {
            return _crudService.GetUsers();
        }

        [HttpGet("3.1 Search user")]
        public List<UserDto> searchUser(string searchS)
        {
            return _crudService.SearchUserByNameOrCode(searchS);
        }

        [HttpGet("3.2 Get user list by role")]
        public Task<IList<UserDto>> getUserListByRole(string roleName)
        {
            return _crudService.GetUsersByRole(roleName);
        }

        [HttpPost("3.3 Add user")]
        public string addUser(UserDto user)
        {
            return _crudService.addUser(user,null);
        }

        [HttpPatch("3.4 Change user role")]
        public Task<string> changeUserRole(string oldRole, string newRole, string userId)
        {
            _crudService.removeUserRole(oldRole, userId);
            return _crudService.addUserRole(newRole, userId);
        }

        [HttpDelete("3.5 Delete user")]
        public string deleteUser(string userId)
        {
            return _crudService.deleteUser(userId);
        }
    }
}
