using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Notification
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        //Foreign key
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto UserDto { get; set; }
    }
}
