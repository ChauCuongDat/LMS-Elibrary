using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class StarSubject
    {
        [Key] public int Id { get; set; }

        //Foreign key
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto userDto { get; set; }
    }
}
