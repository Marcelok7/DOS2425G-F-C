using System.ComponentModel.DataAnnotations;

namespace TMS.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        
        public string? Text { get; set; }
        //? Significa que o atributo pode estar vazio
        public TaskItem? Task { get; set; }
        public User? user { get; set; }
    }
}