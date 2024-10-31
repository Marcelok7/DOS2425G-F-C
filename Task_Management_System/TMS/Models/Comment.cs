namespace TMS.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public TaskItem? Task { get; set; }
    }
}
