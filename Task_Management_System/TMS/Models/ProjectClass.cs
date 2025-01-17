using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class ProjectClass
    {
        // Properties
        [Key]
        public int Id { get; set; } // Auto-implemented property for Id
        
        public string? ProjectName { get; set; } // Public property for name
        public string? Description { get; set; } // Public property for description
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        //? serve para não teres de popular a lista 
        public List<TaskItem>? Tasks { get; set; }

        public ProjectClass() { }
    }
}

