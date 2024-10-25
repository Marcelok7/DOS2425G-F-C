namespace TMS.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string TicketNumber { get; set; }
        public string CommentMade { get; set; }
        public DateTime Date { get; set; }
    }
}
