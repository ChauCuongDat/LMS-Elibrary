using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class StudyingSubject
    {
        [Key] public int Id { get; set; }
        public bool? IsFavorite {  get; set; }
        public DateTime? LastAccessed { get; set; }

        //Foreign key
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto userDto { get; set; }
    }
}
