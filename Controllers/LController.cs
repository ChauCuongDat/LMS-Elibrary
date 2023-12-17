using LMS_Elibrary.Models;
using LMS_Elibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            return _crudService.getSubjects();
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
    }
}
