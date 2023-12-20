using LMS_Elibrary.Models;
using LMS_Elibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_Elibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Student")]
    public class SController : ControllerBase
    {
        private readonly ICRUDService _crudService;
        private readonly IFileHandlerService _fileHandlerService;
        public SController(ICRUDService CRUDService, IFileHandlerService fileHandlerService)
        {
            _crudService = CRUDService;
            _fileHandlerService = fileHandlerService;
        }

        //Function 1: Manage my subject

        [HttpGet("1 Get my subject list")]
        public List<StudyingSubject> getSubjects(string userId)
        {
            return _crudService.getStudyingSubjects(userId);
        }

        [HttpGet("1.2 Search my subject")]
        public List<StudyingSubject> searchSubject(string searchS, string userId)
        {
            return _crudService.searchStudyingSub(searchS, userId);
        }

        [HttpGet("1.3 Filter my subject by favorite")]
        public List<StudyingSubject> filterStudyingSubByFav (string userId, bool isfav)
        {
            return _crudService.getUserSubjectByFav(userId, isfav);
        }

        [HttpGet("1.4 Sort my subject by name or last access")]
        public List<StudyingSubject> sortMySubject (string userId, string sortBy)
        {
            switch(sortBy)
            {
                case "Name":
                    return _crudService.getStudyingSubjectOrderbyAccess(userId);
                case "LastAccess":
                    return _crudService.getStudyingSubjectOrderbyName(userId);
                default:
                    return null;
            }
        }

        [HttpPatch("1.5 Mark favorite subject")]
        public string markFavSub(string userId, int subId)
        {
            return _crudService.favStudyingSubject(userId, subId);
        }

        [HttpGet("1.6 Get Subject detail")]
        public Subject getSubject(int subId)
        {
            return _crudService.GetSubject(subId);
        }

        [HttpGet("1.6.1 Search subject")]
        public List<Subject> searchSubject(string searchS)
        {
            return _crudService.searchSubject(searchS);
        }

        [HttpPost("1.6.3 Get Q&A List")]
        public List<Question> GetQuestionList()
        {
            return _crudService.GetQuestions();
        }

        [HttpGet("1.6.3.1 Search Question")]
        public List<Question> searchQuestion (string searchS)
        {
            return _crudService.searchQ(searchS);
        }

        [HttpPost("1.6.3.2 Post question")]
        public string postQuestion(Question question)
        {
            return _crudService.addQuestion(question);
        }

        [HttpGet("1.6.3.3 See answer")]
        public List<Answer> getAnswerList(int quesId)
        {
            return _crudService.GetAnswersByQuestion(quesId);
        }

        [HttpPost("1.6.3.4 Get Q&A List of a subject")]
        public List<Question> GetQuestionListOfSub(int subId)
        {
            return _crudService.GetQuestionBySubject(subId);
        }

        [HttpGet("1.7 Download subject document")]
        public async Task<IActionResult> downloadDocs (int subId)
        {
            var docs = _crudService.getDocBySub(subId);
            var filePaths = docs.Select(i=>i.FileAddress).ToList();
            foreach (var f in filePaths)
            {
                FileForDownload file = await _fileHandlerService.DownloadFile(f);
                return File(file.bytes, file.contentType, file.fileName);
            }
            return Ok("Files downloaded");
        }
    }
}
