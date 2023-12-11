using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Setting
    {
        [Key] public int Id { get; set; }
        public bool IsNotify { get; set; }

        //Foreign key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto userDto { get; set; }
    }
}
