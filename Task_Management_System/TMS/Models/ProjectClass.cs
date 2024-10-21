namespace TMS.Models
{
    public class ProjectClass
    {
        // Constructor
        public ProjectClass(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        // Properties
        public int Id { get; set; } // Auto-implemented property for Id
        public string Name { get; set; } // Public property for name
        public string Description { get; set; } // Public property for description
    }
}

