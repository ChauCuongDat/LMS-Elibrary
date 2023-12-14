using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class ClassSub
    {
        [Key] public int Id { get; set; }

        //Foreign key
        public int SubId { get; set; }
        [ForeignKey("SubId")]
        public Subject Subject { get; set; }
        
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Class Class { get; set; }
    }
}
