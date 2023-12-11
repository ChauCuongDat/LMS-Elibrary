using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Subject
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        //Foreign key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto UserDto { get; set; }

        //Connection
        public ICollection<Document>? Documents { get; set; }
        public ICollection<Exam>? Exams { get; set; }
        public ICollection<StarSubject>? StarSubjects { get; set; }
        public ICollection<Q_A>? Q_As {  get; set; }
    }
}
