using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_Elibrary.Models
{
    public class Class
    {
        [Key] public int Id { get; set; }
        public string Name { get; set; }

        //Connection
        public ICollection<ClassSub>? ClassSub { get; set; }
    }
}
