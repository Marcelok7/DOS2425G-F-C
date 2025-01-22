using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class User
    {
        [Key]
        public int ID{ get; set; }
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserRole { get; set; }
        public List<TaskItem> Tasks { get; set; } = new List<TaskItem>(); // Lista 

    }
}
