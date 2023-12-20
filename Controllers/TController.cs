using LMS_Elibrary.Models;
using LMS_Elibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace LMS_Elibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Teacher")]
    public class TController : ControllerBase
    {
        private readonly ICRUDService _crudService;
        private readonly IFileHandlerService _fileHandlerService;
        public TController(ICRUDService CRUDService, IFileHandlerService fileHandlerService)
        {
            _crudService = CRUDService;
            _fileHandlerService = fileHandlerService;
        }

        //Function 1: Manage teaching subject

        [HttpGet("1 Get my teaching subject and document")]
        public List<Subject> getMySubject(string userId)
        {
            return _crudService.getUserSubjects(userId);
        }

        [HttpGet("1.1 Search subject")]
        public List<Subject> searchSub(string userId, string searchS)
        {
            return _crudService.searchUserSubject(userId, searchS);
        }

        [HttpGet("1.2 Sort subject by name")]
        public List<Subject> sortSubjectByName(string userId)
        {
            return _crudService.sortSubjectByName(userId);
        }

        [HttpGet("1.3 Get subject detail")]
        public Subject GetSubject(int subId)
        {
            return _crudService.GetSubject(subId);
        }

        [HttpPatch("1.3.1.1 Update subject")]
        public string updateSubject(Subject subject)
        {
            return _crudService.updateSubject(subject);
        }

        [HttpPatch("1.3.1.2 Update topic")]
        public string updateTopic(Topic topic)
        {
            return _crudService.updateTopic(topic);
        }

        [HttpGet("1.4 Get documents of subject")]
        public List<Document> getDocsOfSub(int subId)
        {
            return _crudService.getDocBySub(subId);
        }

        [HttpGet("1.4.1 Filter doc by status")]
        public List<Document> filterDocByStatus(int subId, bool? status)
        {
            return _crudService.getSubDocByStatus(subId, status);
        }

        [HttpPost("1.4.2 Add document")]
        public string addDoc(Document doc, IFormFile file)
        {
            return _crudService.addDoc(doc, file);
        }

        [HttpGet("1.4.3 Download doc")]
        public async Task<IActionResult> DownloadDoc(int docId)
        {
            string filePath = _crudService.getDoc(docId).FileAddress;
            FileForDownload file = await _fileHandlerService.DownloadFile(filePath);
            return File(file.bytes, file.contentType, file.fileName);
        }

        [HttpDelete("1.4.4 Delete document")]
        public string deleteDoc(int docId)
        {
            return _crudService.removeDoc(docId);
        }

        [HttpGet("1.4.5 Seach document")]
        public List<Document> searchDoc(string searchS)
        {
            return _crudService.SearchDoc(searchS);
        }

        [HttpPatch("1.6 Distribute docment to class")]
        public string distributeDoc(ClassSub classSub)
        {
            return _crudService.addClassSubject(classSub);
        }

        [HttpGet("1.6.1 Get class teaching information")]
        public ClassSub getClassSub (int classSubId)
        {
            return _crudService.getClassSub(classSubId);
        }

        //Function 2: Manage lesson and resources

        [HttpGet("1 Get all lesson")]
        public List<Document> getLesson(string userId)
        {
            return _crudService.getLessonByUser(userId);
        }

        [HttpPost("1.1 Upload lesson")]
        public string uploadLesson(Document doc, IFormFile file)
        {
            doc.Type = "lesson";
            return _crudService.addDoc(doc, file);
        }

        [HttpGet("1.2 Dowmload lesson")]
        public async Task<IActionResult> downloadLesson(int docId)
        {
            string filePath = _crudService.getDoc(docId).FileAddress;
            FileForDownload file = await _fileHandlerService.DownloadFile(filePath);
            return File(file.bytes, file.contentType, file.fileName);
        }

        [HttpGet("1.3 Search Lesson")]
        public List<Document> searchLesson (string searchS)
        {
            return _crudService.SearchLesson(searchS);
        }

        [HttpGet("1.4 Filter lesson by doc")]
        public List<Document> filterLessonBySub(int subId)
        {
            return _crudService.getLessonBySub(subId);
        }

        [HttpPatch("1.6 Change lesson name")]
        public string changeLessonName(int docId,  string name)
        {
            Document lesson = _crudService.getDoc(docId);
            lesson.Name = name;
            return _crudService.updateDoc(lesson);
        }

        [HttpDelete("1.7 Delete lesson")]
        public string deleteLesson(int docId)
        {
            return _crudService.removeDoc(docId);
        }

        [HttpPatch("1.8 Add lesson to subject")]
        public string AddLessonToSub (int docId, int subId)
        {
            Document lesson = _crudService.getDoc(docId);
            lesson.SubId = subId;
            return _crudService.updateDoc(lesson);
        }

        //Function 3: Manage Exam

        [HttpGet("1 Get exam list")]
        public List<Exam> getExamList()
        {
            return _crudService.getExamList();
        }

        [HttpGet("1.1 Get exam by subject")]
        public List<Exam> getExamBySubject(int subId)
        {
            return _crudService.getExamsBySub(subId);
        }

        [HttpPost("1.2 Upload exam")]
        public string uploadExam(Exam exam, IFormFile file)
        {
            return _crudService.addExam(exam, file);
        }

        [HttpGet("1.3 Seach exam")]
        public List<Exam> searchExam(string searchS)
        {
            return _crudService.searchExam(searchS);
        }

        [HttpGet("1.4 Get exam detail")]
        public Exam GetExam(int examId)
        {
            return _crudService.getExam(examId);
        }

        [HttpPatch("1.5 Change exam name")]
        public string changeExamName(int examId, string newName)
        {
            Exam exam =_crudService.getExam(examId);
            exam.Name = newName;
            return _crudService.updateExam(exam);
        }

        [HttpGet("1.6 Download Exam")]
        public async Task<IActionResult> downloadExam(int examId)
        {
            string filepath = _crudService.getExam(examId).FileAdddress;
            FileForDownload exam = await _fileHandlerService.DownloadFile(filepath);
            return File(exam.bytes, exam.contentType, exam.fileName);
        }

        [HttpDelete("1.8 DeleteExam")]
        public string deleteExam(int examId)
        {
            return _crudService.removeExam(examId);
        }
    }
}
