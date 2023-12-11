using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class PrivateFile
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type {  get; set; } = string.Empty;
        public double Size { get; set; }
        public DateTime Updated { get; set; }
        public string FileAddress { get; set; } = string.Empty;

        //Foreign key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto UserDto { get; set; }
    }
}
