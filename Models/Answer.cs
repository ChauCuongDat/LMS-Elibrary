using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Answer
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }

        //Foreign key
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}
