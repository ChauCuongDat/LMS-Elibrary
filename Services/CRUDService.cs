using LMS_Elibrary.Contextes;
using LMS_Elibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Document = LMS_Elibrary.Models.Document;

namespace LMS_Elibrary.Services
{
    public class CRUDService
    {
        private readonly LMSDbConext _context;
        private readonly IFileHandlerService _fileHandlerService;

        public CRUDService(LMSDbConext LMSDbConext, IFileHandlerService fileHandlerService)
        {
            this._context = LMSDbConext;
            this._fileHandlerService = fileHandlerService;
        }

        //Document
        public string addDoc (Document doc, IFormFile docFile)
        {
            doc.FileAddress = _fileHandlerService.SaveDocument(docFile).ToString();
            _context.document.Add(doc);
            _context.SaveChanges();
            return "Document added";
        }
        public List<Document> getDocs ()
        {
            return _context.document.ToList();
        }
        public List<Document> getSubjectDocs (int subId)
        {
            return _context.document.
                Include(i => i.Subject).
                Where(i => i.SubId == subId).
                ToList();
        }
        public List<Document> getApprDocs ()
        {
            return _context.document.
                Where(i => i.IsApproved == true).
                ToList();
        }
        public List<Document> getYetApprDoc()
        {
            return _context.document.
                Where(i => i.IsApproved == null).
                ToList();
        }
        public Document getDoc (int Id)
        {
            return _context.document.FirstOrDefault(i => i.Id == Id);
        }
        public string updateDoc (Document doc)
        {
            _context.document.Update(doc);
            _context.SaveChanges();
            return "Document updated";
        }
        public string approveDoc (int docId, string approverId)
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
        public string disapprDoc (int docId, string approverId)
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
        public string removeDoc (int Id)
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
        public List<Exam> getExamList ()
        {
            return _context.exam.ToList();
        }
        public List<Exam> getExamsBySub (int subId)
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
        public List<Exam> getExamsByStatusA()
        {
            return _context.exam.
                Where(i => i.ApproStatus == true).
                ToList();
        }
        public List<Exam> getExamsByStatusD()
        {
            return _context.exam.
                Where(i => i.ApproStatus == false).
                ToList();
        }
        public List<Exam> getExamsByStatusN()
        {
            return _context.exam.
                Where(i => i.ApproStatus == null).
                ToList();
        }
        public Exam getExam (int Id)
        {
            return _context.exam.FirstOrDefault(i => i.Id == Id);
        }
        public string updateExam (Exam exam)
        {
            _context.exam.Update(exam);
            _context.SaveChanges();
            return "Exam updated";
        }
        public string approExam (int Id)
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
        public string disapproExam (int Id)
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
        public string removeExam (int Id)
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
        public Help getHelp (int id)
        {
            return _context.help.FirstOrDefault(i => i.Id == id);
        }
        public string updateHelp (Help help)
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
        public Notification getNoti(int id)
        {
            Notification noti = _context.notification.FirstOrDefault(i => i.Id == id);
            noti.IsRead = true;
            _context.notification.Update(noti);
            _context.SaveChanges();
            return noti;
        }
        public string updateNotification (Notification noti)
        {
            _context.notification.Update(noti);
            _context.SaveChanges();
            return "Notification updadted";
        }
        public string readNoti(int notiId)
        {
            Notification noti =_context.notification.Find(notiId);
            noti.IsRead=true;
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
            if(noti != null)
            {
                _context.notification.Remove(noti);
                _context.SaveChanges();
                return "Notification removed";
            }
            return "No notification found";
        }

        //Private file
        public string addPriFile (PrivateFile priFile, IFormFile privateFile)
        {
            priFile.FileAddress = _fileHandlerService.SavePriFile(privateFile).ToString();
            _context.privateFile.Add(priFile);
            _context.SaveChanges();
            return "Private file added";
        }
        public List<PrivateFile> getPriFiles()
        {
            return _context.privateFile.ToList();
        }
        public List<PrivateFile> GetPriFilesByTypes (string type)
        {
            return _context.privateFile.
                Where(i => i.Type == type)
                .ToList();
        }
        public PrivateFile getPrivateFile(int id)
        {
            return _context.privateFile.FirstOrDefault(i => i.Id == id);
        }
        public string updatePriFile (PrivateFile priFile)
        {
            _context.privateFile.Update(priFile);
            _context.SaveChanges();
            return "Private file updated";
        }
        public string changePriFileName (int priFileId, string name)
        {
            PrivateFile priFile = _context.privateFile.Find(priFileId);
            if (priFile == null)
            {
                return "No file found";
            }
            priFile.Name = name;
            _context.privateFile.Update (priFile);
            _context.SaveChanges ();
            return "File name changed";
        }
        public string removePriFile (int id)
        {
            PrivateFile priFile = _context.privateFile.Find(id);
            if (priFile != null)
            {
                _context.privateFile.Remove(priFile);
                _context.SaveChanges();
                return "Private file removed";
            }
            return "No private file found";
        }

        //Q_A
        public string addQ_A(Q_A Q_A)
        {
            _context.q_a.Add(Q_A);
            _context.SaveChanges();
            return "Q&A added";
        }
        public List<Q_A> GetQ_As()
        {
            return _context.q_a.ToList();
        }
        public Q_A GetQ_A(int id)
        {
            return _context.q_a.FirstOrDefault(i => i.Id == id);
        }
        public string updateQ_A (Q_A Q_A)
        {
            _context.q_a.Update(Q_A);
            _context.SaveChanges();
            return "Q&A updated";
        }
        public string removeQ_A(int id)
        {
            Q_A q_a = _context.q_a.Find(id);
            if (q_a != null)
            {
                _context.Remove(q_a);
                _context.SaveChanges();
                return "Q&A removed";
            }
            return "Q&A not found";
        }

        //Star subject
        public string addStarSubject(StarSubject starSub)
        {
            _context.starSubject.Add(starSub);
            _context.SaveChanges();
            return "Star subject added";
        }
        public List<StarSubject> GetStarSubjects()
        {
            return _context.starSubject.ToList();
        }
        public StarSubject GetStarSubject(int id)
        {
            return _context.starSubject.FirstOrDefault(i => i.Id == id);
        }
        public string updateStarSUbject(StarSubject starSub)
        {
            _context.starSubject.Update(starSub);
            _context.SaveChanges();
            return "Star subject added";
        }
        public string deleteStarSubject(int id)
        {
            StarSubject starSubject = _context.starSubject.Find(id);
            if (starSubject != null)
            {
                _context.starSubject.Remove(starSubject);
                _context.SaveChanges();
                return "Star subject removed";
            }
            return "No star subject found";
        }
   

        //Subject
        public string addSubject(Subject sub)
        {
            _context.subject.Add(sub);
            _context.SaveChanges();
            return "Subject added";
        }
        public List<Subject> GetSSubjects()
        {
            return _context.subject.ToList();
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
    }
}
