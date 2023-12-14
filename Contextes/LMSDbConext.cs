using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using LMS_Elibrary.Models;
using Document = LMS_Elibrary.Models.Document;

namespace LMS_Elibrary.Contextes
{
    public class LMSDbConext:IdentityDbContext
    {
        public LMSDbConext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Document> document { get; set; }
        public DbSet<Exam> exam { get; set; }
        public DbSet<Help> help { get; set; }
        public DbSet<Notification> notification { get; set; }
        public DbSet<PrivateFile> privateFile { get; set; }
        public DbSet<Question> question { get; set; }
        public DbSet<StudyingSubject> studyingSubject { get; set; }
        public DbSet<Subject> subject { get; set; }
        public DbSet<UserDto> userDto { get; set; }
        public DbSet<Setting> settings { get; set; }
        public DbSet<Topic> topic { get; set; }
        public DbSet<Answer> answer { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<ClassSub> classSub { get; set; }
    }
}
