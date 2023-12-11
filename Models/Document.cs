using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Document
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type {  get; set; } = string.Empty;
        public DateTime? ApproDate { get; set; }
        public bool? IsApproved { get; set; }
        public string? ApproverId { get; set;}
        public string FileAddress { get; set; } = string.Empty;

        //Foreign Key
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }
        
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public UserDto UserDto { get; set; }
    }
}
