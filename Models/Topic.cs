using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Topic
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //Foreign key
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }
    }
}
