using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Question
    {
        [Key] public int Id { get; set; }
        public string Detail { get; set; } = string.Empty;

        //Foreign key
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto userDto { get; set; }

        //Connection
        public ICollection<Answer>? Answers { get; set; }
    }
}
