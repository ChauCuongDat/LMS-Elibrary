﻿using LMS_Elibrary.Models;
using Microsoft.AspNetCore.Identity;

namespace LMS_Elibrary.Services
{
    public interface ICRUDService
    {
        string addAnswer(Answer answer);
        string addClass(Class clazz);
        string addClassSubject(ClassSub classSub);
        string addDoc(Document doc, IFormFile docFile);
        string addExam(Exam exam, IFormFile examFile);
        string addHelp(Help help);
        string addNoti(Notification noti);
        string addPriFile(PrivateFile priFile, IFormFile privateFile);
        string addQuestion(Question quest);
        string addRole(IdentityRole iRole);
        string addSetting(Setting setting);
        string addStudyingSubject(StudyingSubject stuSub);
        string addSubject(Subject sub);
        string addTopic(Topic topic);
        string addUser(UserDto user, IFormFile avatar);
        Task<string> addUserRole(string roleName, string userId);
        string approExam(int Id);
        string approveDoc(int docId, string approverId);
        Task<string> changeAvatar(IFormFile avatar, string userId);
        Task<IdentityResult> changePassword(string userId, string oldPass, string newPass);
        string changePriFileName(int priFileId, string name);
        string changeSetting(Setting setting);
        string deleteStudyingSubject(int id);
        string deleteSubject(int id);
        string deleteUser(int id);
        string disapprDoc(int docId, string approverId);
        string disapproExam(int Id);
        string favStudyingSubject(int studyId);
        List<Answer> GetAnswersByQuestion(int quesId);
        List<Class> GetClassesBy();
        List<ClassSub> getClassSubs();
        Document getDoc(int Id);
        List<Document> getDocByStatus(bool? status);
        List<Document> getDocBySub(int subId);
        List<Document> getDocByUser(string userId);
        List<Document> getDocs();
        Exam getExam(int Id);
        List<Exam> getExamList();
        List<Exam> getExamsByStatus(bool? status);
        List<Exam> getExamsBySub(int subId);
        List<Exam> getExamsByTeach(string UserId);
        Help getHelp(int id);
        List<Help> getHelps();
        Notification getNoti(int id);
        List<Notification> getNotisByUser(string userId);
        List<PrivateFile> getPriFilesByTypes(string type);
        PrivateFile getPrivateFile(int id);
        Question GetQuestion(int id);
        List<Question> GetQuestions();
        List<IdentityRole> GetRoles();
        List<Subject> getSubjects();
        StudyingSubject getStudyingSubject(int subId, string userId);
        List<StudyingSubject> getStudyingSubjectOrderbyName();
        List<StudyingSubject> getStudyingSubjects();
        Subject GetSubject(int id);
        List<Topic> GetSubTopics(int subId);
        UserDto GetUser(string id);
        List<UserDto> GetUsers();
        Task<IList<UserDto>> GetUsersByRole(string roleName);
        List<StudyingSubject> getUserSubjectByFav(string userId, bool? isFav);
        string readNoti(int notiId);
        string removeAnswer(int id);
        string removeClass(int classId);
        string removeClassSub(int classSubId);
        string removeDoc(int Id);
        string removeExam(int Id);
        string removeHelp(int id);
        string removeNotification(int id);
        string removePriFile(int id);
        string removeQuestion(int id);
        string removeRole(IdentityRole role);
        string removeTopic(Topic topic);
        Task<string> removeUserRole(string roleName, string userId);
        List<Answer> searchA(string searchS);
        List<Exam> searchExam(string searchStr);
        List<Notification> searchNoti(string searchS);
        List<Question> searchQ(string searchS);
        List<StudyingSubject> searchSub(string searchS);
        List<Subject> searchSubject(string searchS);
        List<UserDto> SearchUserByNameOrCode(string searchS);
        string UnfavStudyingSubject(int studyId);
        string unreadNoti(int notiId);
        string updateAnswer(Answer answer);
        string updateClass(Class clazz);
        string updateClass(ClassSub classSub);
        string updateDoc(Document doc);
        string updateExam(Exam exam);
        string updateHelp(Help help);
        string updateNotification(Notification noti);
        string updatePriFile(PrivateFile priFile);
        string updateQuestion(Question quest);
        string updateRole(IdentityRole role);
        string updateStudyingSubject(StudyingSubject stuSub);
        string updateSUbject(Subject subject);
        string updateTopic(Topic topic);
        string updateUser(UserDto user);
        List<PrivateFile> getPriFiles(string userId);
        List<PrivateFile> searchPrifile(string searchS);
    }
}