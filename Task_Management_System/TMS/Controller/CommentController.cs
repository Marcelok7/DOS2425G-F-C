using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TMS.Models;

namespace TMS.Controller;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private static readonly List<Comment> comments = new List<Comment>
    {
        new Comment
        {
            Id = 1,
            TicketNumber = "JS-1203",
            CommentMade = "Comentário ticket JS-1203",
            Date = DateTime.Now.AddDays(2)
        },
        new Comment
        {
            Id = 2,
            TicketNumber = "AB-352",
            CommentMade = "Comentário ticket AB-352",
            Date = DateTime.Now.AddDays(-1)
        },
        new Comment
        {
            Id = 3,
            TicketNumber = "AA-9855",
            CommentMade = "Comentário ticket AA-9855",
            Date = DateTime.Now.AddDays(5)
        }
    };
    
    [HttpGet]
    public IActionResult GetAllComments()
    {
        return Ok(comments);
    }
}
