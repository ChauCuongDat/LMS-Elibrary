using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Exam
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Form { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public bool? ApproStatus { get; set; }
        public bool Public {  get; set; }
        public string FileAdddress { get; set; } = string.Empty;

        //Foreign key
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto UserDto { get; set; }

        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }
    }
}
