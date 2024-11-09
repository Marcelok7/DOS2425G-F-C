namespace TMS.Models
{
    public class ProjectClass
    {


        // Properties
        public int Id { get; set; } // Auto-implemented property for Id
        public string? Name { get; set; } // Public property for name
        public string? Description { get; set; } // Public property for description
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        //? serve para não teres de popular a lista 
        public List<Task>? Tasks { get; set; }

      
    }
}
