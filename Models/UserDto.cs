using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class UserDto : IdentityUser
    {
        public string Code { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Avatar { get; set; } = string.Empty;

        //Connection
        public ICollection<Subject>? Subjects { get; set; }
        public ICollection<Document>? Documents { get; set;}
        public ICollection<PrivateFile>? PrivateFiles { get; set; }
        public ICollection<Exam>? Exams { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        public ICollection<StudyingSubject>? StarSubjects { get; set; }
        public ICollection<Question>? Q_As { get; set; }
        public ICollection<Help>? Helps { get; set; }
        public Setting Setting { get; set; }
    }
}
