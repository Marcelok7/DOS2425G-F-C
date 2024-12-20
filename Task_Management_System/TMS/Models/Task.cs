using System.ComponentModel.DataAnnotations;

namespace TMS.Models;

public class TaskItem
{
    [Key]
    public int Id { get; set; }
    public string TicketNumber { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; }
    public string Assignee { get; set; }
    public int UserId { get; set; } // ID do user associado
    public List<Comment> comments { get; set; } = new List<Comment>(); // Lista
}
