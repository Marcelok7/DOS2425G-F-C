using Microsoft.AspNetCore.Mvc;
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
            Task = new TaskItem{Id = 1},
            Text = "Comentário ticket JS-1203"
        },
        new Comment
        {
            Id = 2,
            Task = new TaskItem{Id = 2},
            Text = "Comentário ticket AB-352"
        },
        new Comment
        {
            Id = 3,
            Task = new TaskItem{Id = 3},
            Text = "Comentário ticket AA-9855"
        }
    };
    
    [HttpGet]
    public IActionResult GetAllComments()
    {
        return Ok(comments);
    }

    // GET: api/comment/{id}
    [HttpGet("{id}")]
    public IActionResult GetComment(int id)
    {
        var comment = comments.FirstOrDefault(u => u.Id == id);
        if (comment == null) return NotFound();
        return Ok(comment);
    }

    // POST: api/comment
    [HttpPost]
    public IActionResult CreateComment(Comment newComment)
    {
        if(comments.Find(u => u.Id == newComment.Id) != null) { 
            return BadRequest();
        } 
       else{
            newComment.Id = comments.Max(u => u.Id) + 1;
            comments.Add(newComment);
            return CreatedAtAction(nameof(GetComment), new { id = newComment.Id }, newComment);
        }
    }

    // PUT: api/comment/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateComment(int id, Comment updatedComment)
    {
        
        if(id != updatedComment.Id){
            return BadRequest();
        }
        else
        {
            var comment = comments.FirstOrDefault(u => u.Id == id);

            if (comment == null) return NotFound();

            comment.Text = updatedComment.Text;

            return Ok(comment);
        }
    }

    // DELETE: api/comment/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteComment(int id)
    {
        var comment = comments.FirstOrDefault(u => u.Id == id);
        if (comment == null)
        {
            return NotFound();
        }
        else
        {
            comments.Remove(comment);
            return NoContent();
        }
    }
}