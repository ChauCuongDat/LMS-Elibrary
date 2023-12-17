using LMS_Elibrary.Contextes;
using LMS_Elibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Document = LMS_Elibrary.Models.Document;

namespace LMS_Elibrary.Services
{
    public class CRUDService : ICRUDService
    {
        private readonly LMSDbConext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<UserDto> _userManager;
        private readonly IFileHandlerService _fileHandlerService;

        public CRUDService(LMSDbConext LMSDbConext, IFileHandlerService fileHandlerService, RoleManager<IdentityRole> roleManager, UserManager<UserDto> userManager)
        {
            this._context = LMSDbConext;
            this._fileHandlerService = fileHandlerService;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        //Document
        public string addDoc(Document doc, IFormFile docFile)
        {
            doc.FileAddress = _fileHandlerService.SaveDocument(docFile).ToString();
            _context.document.Add(doc);
            _context.SaveChanges();
            return "Document added";
        }
        public List<Document> getDocs()
        {
            return _context.document.ToList();
        }
        public List<Document> getDocBySub(int subId)
        {
            return _context.document.
                Include(i => i.Subject).
                Where(i => i.SubId == subId).
                ToList();
        }
        public List<Document> getDocByStatus(bool? status)
        {
            return _context.document.
                Where(i => i.IsApproved == status).
                ToList();
        }
        public List<Document> getDocByUser(string userId)
        {
            return _context.document.
                Include(i => i.UserDto).
                Where(i => i.UserId == userId).ToList();
        }
        public Document getDoc(int Id)
        {
            return _context.document.FirstOrDefault(i => i.Id == Id);
        }
        public string updateDoc(Document doc)
        {
            _context.document.Update(doc);
            _context.SaveChanges();
            return "Document updated";
        }
        public string approveDoc(int docId, string approverId)
        {
            Document doc = _context.document.Find(docId);
            if (doc == null)
            {
                return "No document found";
            }
            doc.IsApproved = true;
            doc.ApproDate = DateTime.Now;
            doc.ApproverId = approverId;
            _context.document.Update(doc);
            _context.SaveChanges();
            return "Document approved";
        }
        public string disapprDoc(int docId, string approverId)
        {
            Document doc = _context.document.Find(docId);
            if (doc == null)
            {
                return "No document found";
            }
            doc.IsApproved = false;
            doc.ApproDate = DateTime.Now;
            doc.ApproverId = approverId;
            _context.document.Update(doc);
            _context.SaveChanges();
            return "Document disapproved";
        }
        public string removeDoc(int Id)
        {
            Document doc = _context.document.Find(Id);
            if (doc != null)
            {
                _context.document.Remove(doc);
                _context.SaveChanges();
                return "Document removed";
            }
            return "No document found";
        }

        //Exam
        public string addExam(Exam exam, IFormFile examFile)
        {
            exam.FileAdddress = _fileHandlerService.SaveExam(examFile).ToString();
            exam.Created = DateTime.Now;
            _context.exam.Add(exam);
            _context.SaveChanges();
            return "Exam added";
        }
        public List<Exam> getExamList()
        {
            return _context.exam.ToList();
        }
        public List<Exam> getExamsBySub(int subId)
        {
            return _context.exam.
                Include(i => i.Subject).
                Where(i => i.SubId == subId).
                ToList();
        }
        public List<Exam> getExamsByTeach(string UserId)
        {
            return _context.exam.
                Include(i => i.UserDto).
                Where(i => i.UserId == UserId).
                ToList();
        }
        public List<Exam> getExamsByStatus(bool? status)
        {
            return _context.exam.
                Where(i => i.ApproStatus == status).
                ToList();
        }
        public List<Exam> searchExam(string searchStr)
        {
            return _context.exam.Where(i => i.Name.Contains(searchStr)).ToList();
        }
        public Exam getExam(int Id)
        {
            return _context.exam.FirstOrDefault(i => i.Id == Id);
        }
        public string updateExam(Exam exam)
        {
            _context.exam.Update(exam);
            _context.SaveChanges();
            return "Exam updated";
        }
        public string approExam(int Id)
        {
            Exam exam = _context.exam.Find(Id);
            if (exam == null)
            {
                return "No exam found";
            }
            exam.ApproStatus = true;
            _context.exam.Update(exam);
            _context.SaveChanges();
            return "Exam approved";
        }
        public string disapproExam(int Id)
        {
            Exam exam = _context.exam.Find(Id);
            if (exam == null)
            {
                return "No exam found";
            }
            exam.ApproStatus = false;
            _context.exam.Update(exam);
            _context.SaveChanges();
            return "Exam disapproved";
        }
        public string removeExam(int Id)
        {
            Exam exam = _context.exam.Find(Id);
            if (exam != null)
            {
                _context.exam.Remove(exam);
                _context.SaveChanges();
                return "Exam removed";
            }
            return "No exam found";
        }

        //Help
        public string addHelp(Help help)
        {
            _context.help.Add(help);
            _context.SaveChanges();
            return "Help added";
        }
        public List<Help> getHelps()
        {
            return _context.help.ToList();
        }
        public Help getHelp(int id)
        {
            return _context.help.FirstOrDefault(i => i.Id == id);
        }
        public string updateHelp(Help help)
        {
            _context.help.Update(help);
            _context.SaveChanges();
            return "Help updated";
        }
        public string removeHelp(int id)
        {
            Help help = _context.help.Find(id);
            if (help != null)
            {
                _context.help.Remove(help);
                _context.SaveChanges();
                return "Help removed";
            }
            return "Help no found";
        }

        //Notification
        public string addNoti(Notification noti)
        {
            noti.IsRead = false;
            _context.notification.Add(noti);
            _context.SaveChanges();
            return "Notification added";
        }
        public List<Notification> getNotisByUser(string userId)
        {
            return _context.notification.
                Include(i => i.UserDto).
                Where(i => i.UserId == userId).
                ToList();
        }
        public List<Notification> searchNoti(string searchS)
        {
            return _context.notification.Where(i => i.Name.Contains(searchS)).ToList();
        }
        public Notification getNoti(int id)
        {
            Notification noti = _context.notification.FirstOrDefault(i => i.Id == id);
            noti.IsRead = true;
            _context.notification.Update(noti);
            _context.SaveChanges();
            return noti;
        }
        public string updateNotification(Notification noti)
        {
            _context.notification.Update(noti);
            _context.SaveChanges();
            return "Notification updadted";
        }
        public string readNoti(int notiId)
        {
            Notification noti = _context.notification.Find(notiId);
            noti.IsRead = true;
            _context.notification.Update(noti);
            _context.SaveChanges();
            return "Notidication read";
        }
        public string unreadNoti(int notiId)
        {
            Notification noti = _context.notification.Find(notiId);
            noti.IsRead = false;
            _context.notification.Update(noti);
            _context.SaveChanges();
            return "Notidication unread";
        }
        public string removeNotification(int id)
        {
            Notification noti = _context.notification.Find(id);
            if (noti != null)
            {
                _context.notification.Remove(noti);
                _context.SaveChanges();
                return "Notification removed";
            }
            return "No notification found";
        }

        //Private file
        public string addPriFile(PrivateFile priFile, IFormFile privateFile)
        {
            priFile.FileAddress = _fileHandlerService.SavePriFile(privateFile).ToString();
            _context.privateFile.Add(priFile);
            _context.SaveChanges();
            return "Private file added";
        }
        public List<PrivateFile> getPriFiles(string userId)
        {
            return _context.privateFile.
                Include(i=>i.UserDto).
                Where(i=>i.UserId == userId).ToList();
        }
        public List<PrivateFile> getPriFilesByTypes(string type)
        {
            return _context.privateFile.
                Where(i => i.Type == type)
                .ToList();
        }
        public List<PrivateFile> searchPrifile(string searchS)
        {
            return _context.privateFile.Where(i=>i.Name.Contains(searchS)).ToList();
        }
        public PrivateFile getPrivateFile(int id)
        {
            return _context.privateFile.FirstOrDefault(i => i.Id == id);
        }
        public string updatePriFile(PrivateFile priFile)
        {
            _context.privateFile.Update(priFile);
            _context.SaveChanges();
            return "Private file updated";
        }
        public string changePriFileName(int priFileId, string name)
        {
            PrivateFile priFile = _context.privateFile.Find(priFileId);
            if (priFile == null)
            {
                return "No file found";
            }
            priFile.Name = name;
            _context.privateFile.Update(priFile);
            _context.SaveChanges();
            return "File name changed";
        }
        public string removePriFile(int id)
        {
            PrivateFile priFile = _context.privateFile.Find(id);
            if (priFile != null)
            {
                _fileHandlerService.DeleteFile(priFile.FileAddress);
                _context.privateFile.Remove(priFile);
                _context.SaveChanges();
                return "Private file removed";
            }
            return "No private file found";
        }

        //Question
        public string addQuestion(Question quest)
        {
            _context.question.Add(quest);
            _context.SaveChanges();
            return "Q&A added";
        }
        public List<Question> GetQuestions()
        {
            return _context.question.ToList();
        }
        public List<Question> searchQ(string searchS)
        {
            return _context.question.
                Include(i => i.Answers).
                Where(i => i.Detail.Contains(searchS)).ToList();
        }
        public Question GetQuestion(int id)
        {
            return _context.question.FirstOrDefault(i => i.Id == id);
        }
        public string updateQuestion(Question quest)
        {
            _context.question.Update(quest);
            _context.SaveChanges();
            return "Q&A updated";
        }
        public string removeQuestion(int id)
        {
            Question quest = _context.question.Find(id);
            if (quest != null)
            {
                _context.question.Remove(quest);
                _context.SaveChanges();
                return "Question removed";
            }
            return "Question not found";
        }

        //Answer
        public string addAnswer(Answer answer)
        {
            _context.answer.Add(answer);
            _context.SaveChanges();
            return "Answer added";
        }
        public List<Answer> GetAnswersByQuestion(int quesId)
        {
            return _context.answer.
                Include(i => i.Question).
                Where(i => quesId == i.Id).ToList();
        }
        public List<Answer> searchA(string searchS)
        {
            return _context.answer.
                Include(i => i.Question).
                Where(i => i.Detail.Contains(searchS)).ToList();
        }
        public string updateAnswer(Answer answer)
        {
            _context.answer.Update(answer);
            _context.SaveChanges();
            return "Answer updated";
        }
        public string removeAnswer(int id)
        {
            Answer answer = _context.answer.Find(id);
            if (answer != null)
            {
                _context.answer.Remove(answer);
                _context.SaveChanges();
                return "Answer removed";
            }
            return "Answer not found";
        }

        //Studying subject
        public string addStudyingSubject(StudyingSubject stuSub)
        {
            _context.studyingSubject.Add(stuSub);
            _context.SaveChanges();
            return "Studying subject added";
        }
        public List<StudyingSubject> getStudyingSubjects()
        {
            return _context.studyingSubject.Include(i => i.Subject).ToList();
        }
        public List<StudyingSubject> getStudyingSubjectOrderbyName()
        {
            return _context.studyingSubject.
                Include(i => i.Subject).
                OrderByDescending(i => i.Subject.Name).ToList();
        }
        public List<StudyingSubject> searchSub(string searchS)
        {
            return _context.studyingSubject.Include(i => i.Subject).Where(i => i.Subject.Name.Contains(searchS)).ToList();
        }
        public StudyingSubject getStudyingSubject(int subId, string userId)
        {
            StudyingSubject studyingSubject = _context.studyingSubject.
                Include(i => i.Subject).
                Include(i => i.userDto).
                FirstOrDefault(i => i.SubId == subId & i.UserId == userId);
            studyingSubject.LastAccessed = DateTime.Now;
            _context.studyingSubject.Update(studyingSubject);
            _context.SaveChanges();
            return studyingSubject;
        }
        public string updateStudyingSubject(StudyingSubject stuSub)
        {
            _context.studyingSubject.Update(stuSub);
            _context.SaveChanges();
            return "Studying subject updated";
        }
        public string favStudyingSubject(int studyId)
        {
            StudyingSubject stuSub = _context.studyingSubject.Find(studyId);
            stuSub.IsFavorite = true;
            _context.SaveChanges();
            return "Studying subject favorited";
        }
        public string UnfavStudyingSubject(int studyId)
        {
            StudyingSubject stuSub = _context.studyingSubject.Find(studyId);
            stuSub.IsFavorite = false;
            _context.SaveChanges();
            return "Studying subject unfavorited";
        }
        public string deleteStudyingSubject(int id)
        {
            StudyingSubject stuSubject = _context.studyingSubject.Find(id);
            if (stuSubject != null)
            {
                _context.studyingSubject.Remove(stuSubject);
                _context.SaveChanges();
                return "Studying subject removed";
            }
            return "No Studying subject found";
        }


        //Subject
        public string addSubject(Subject sub)
        {
            _context.subject.Add(sub);
            _context.SaveChanges();
            return "Subject added";
        }
        public List<Subject> getSubjects()
        {
            return _context.subject.ToList();
        }
        public List<Subject> searchSubject(string searchS)
        {
            return _context.subject.Where(i => i.Name.Contains(searchS)).ToList();
        }
        public List<StudyingSubject> getUserSubjectByFav(string userId, bool? isFav)
        {
            return _context.studyingSubject.
                Include(i => i.userDto).
                Include(i => i.Subject).
                Where(i => i.UserId == userId & i.IsFavorite == isFav).ToList();
        }
        public Subject GetSubject(int id)
        {
            return _context.subject.FirstOrDefault(i => i.Id == id);
        }
        public string updateSUbject(Subject subject)
        {
            _context.subject.Update(subject);
            _context.SaveChanges();
            return "Subject updated";
        }
        public string deleteSubject(int id)
        {
            Subject subject = _context.subject.Find(id);
            if (subject != null)
            {
                _context.subject.Remove(subject);
                _context.SaveChanges();
                return "Subject removed";
            }
            return "No subject found";
        }

        //UserDto
        public string addUser(UserDto user, IFormFile avatar)
        {
            user.Avatar = _fileHandlerService.SaveAvatar(avatar).ToString();
            _context.userDto.Add(user);
            _context.SaveChanges();
            return "User added";
        }
        public List<UserDto> GetUsers()
        {
            return _context.userDto.ToList();
        }
        public async Task<IList<UserDto>> GetUsersByRole(string roleName)
        {
            return await Task.FromResult(_userManager.GetUsersInRoleAsync(roleName).Result);
        }
        public List<UserDto> SearchUserByNameOrCode(string searchS)
        {
            return _context.userDto.Where(i => i.UserName.Contains(searchS) || i.Code.Contains(searchS)).ToList();
        }
        public UserDto GetUser(string id)
        {
            return _context.userDto.FirstOrDefault(i => i.Id == id);
        }
        public string updateUser(UserDto user)
        {
            _context.userDto.Update(user);
            _context.SaveChanges();
            return "User added";
        }
        public async Task<string> addUserRole(string roleName, string userId)
        {
            UserDto user = _userManager.FindByIdAsync(userId).Result;
            await _userManager.AddToRoleAsync(user, roleName);
            return "Role has been added to user";
        }
        public async Task<string> removeUserRole(string roleName, string userId)
        {
            UserDto user = _userManager.FindByIdAsync(userId).Result;
            await _userManager.RemoveFromRoleAsync(user, roleName);
            return "Role has been removed from user";
        }
        public async Task<string> changeAvatar(IFormFile avatar, string userId)
        {
            UserDto user = _context.userDto.FirstOrDefault(i => i.Id == userId);
            if (user.Avatar != null)
            {
                await _fileHandlerService.DeleteFile(user.Avatar);
            }
            user.Avatar = _fileHandlerService.SaveAvatar(avatar).ToString();
            updateUser(user);
            return "Avatar changed";
        }
        public async Task<IdentityResult> changePassword(string userId, string oldPass, string newPass)
        {
            UserDto user = _context.userDto.FirstOrDefault(i => i.Id == userId);
            return await _userManager.ChangePasswordAsync(user, oldPass, newPass);
        }
        public string deleteUser(int id)
        {
            UserDto user = _context.userDto.Find(id);
            if (user != null)
            {
                _context.userDto.Remove(user);
                _context.SaveChanges();
                return "User removed";
            }
            return "no user found";
        }

        //Setting
        public string addSetting(Setting setting)
        {
            _context.settings.Add(setting);
            _context.SaveChanges();
            return "Setting created";
        }
        public string changeSetting(Setting setting)
        {
            _context.settings.Update(setting);
            _context.SaveChanges();
            return "Setting saved";
        }

        //Role
        public string addRole(IdentityRole iRole)
        {
            _roleManager.CreateAsync(iRole);
            return "Role created";
        }
        public List<IdentityRole> GetRoles()
        {
            return _roleManager.Roles.ToList();
        }
        public string updateRole(IdentityRole role)
        {
            _roleManager.UpdateAsync(role);
            return "Role updated";
        }
        public string removeRole(IdentityRole role)
        {
            _roleManager.DeleteAsync(role);
            return "Role deleted";
        }

        //Topic
        public string addTopic(Topic topic)
        {
            _context.Add(topic);
            _context.SaveChanges();
            return "Topic added";
        }
        public List<Topic> GetSubTopics(int subId)
        {
            return _context.topic.
                Include(i => i.Subject).
                Where(i => i.SubId == subId).ToList();
        }
        public string updateTopic(Topic topic)
        {
            _context.Update(topic);
            _context.SaveChanges();
            return "Topic updated";
        }
        public string removeTopic(Topic topic)
        {
            _context.Remove(topic);
            _context.SaveChanges();
            return "Topic removed";
        }

        //Class
        public string addClass(Class clazz)
        {
            _context.Add(clazz);
            _context.SaveChanges();
            return "Class added";
        }
        public List<Class> GetClassesBy()
        {
            return _context.classes.ToList();
        }
        public string updateClass(Class clazz)
        {
            _context.Update(clazz);
            _context.SaveChanges();
            return "Class updated";
        }
        public string removeClass(int classId)
        {
            Class clazz = _context.classes.Find(classId);
            _context.Remove(clazz);
            _context.SaveChanges();
            return "Class removed";
        }

        //Class subject
        public string addClassSubject(ClassSub classSub)
        {
            _context.Add(classSub);
            _context.SaveChanges();
            return "New subject added to class";
        }
        public List<ClassSub> getClassSubs()
        {
            return _context.classSub.ToList();
        }
        public string updateClass(ClassSub classSub)
        {
            _context.Update(classSub);
            _context.SaveChanges();
            return "Subject of class edited";
        }
        public string removeClassSub(int classSubId)
        {
            ClassSub classSub = _context.classSub.Find(classSubId);
            if (classSub != null)
            {
                _context.Remove(classSub);
                _context.SaveChanges();
                return "Subject has been remove from class";
            }
            return "No subject in class is found";
        }
    }
}
