using Microsoft.AspNetCore.Mvc;
using TMS.Models;
using System.Linq;

namespace TMS.Controller;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private static readonly List<TaskItem> tasks = new List<TaskItem>
    {
        new TaskItem
        {
            Id = 1,
            TicketNumber = "JS-1203",
            Title = "Bug reported - fix",
            Description = "A bug was detected in Service X",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(2),
            Priority = "High",
            Assignee = "John Doe"
        },
        new TaskItem
        {
            Id = 2,
            TicketNumber = "AB-352",
            Title = "New Functionality - use C#",
            Description = "It's necessary to use .NET Core in these lessons",
            IsCompleted = true,
            DueDate = DateTime.Now.AddDays(-1),
            Priority = "Medium",
            Assignee = "Jane Smith"
        },
        new TaskItem
        {
            Id = 3,
            TicketNumber = "AA-9855",
            Title = "Improvements",
            Description = "Create new stories to implement new features",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(5),
            Priority = "Low",
            Assignee = "Alice Brown"
        }
    };

    [HttpGet]
    public IActionResult GetTasks()
    {
        return Ok(tasks);
    }

    // GET: api/tasks/{id}
    [HttpGet("{id}")]
    public IActionResult GetTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    // POST: api/tasks
    [HttpPost]
    public IActionResult CreateTask([FromBody] TaskItem task)
    {
        if (task == null)
        {
            return BadRequest();
        }
        task.Id = tasks.Max(t => t.Id) + 1;
        tasks.Add(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    // PUT: api/tasks/{id}
    [HttpPut]
    public IActionResult UpdateTask([FromBody] TaskItem updatedTask)
    {
        if (updatedTask == null || updatedTask.Id == 0)
        {
            return BadRequest();
        }

        var existingTask = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
        if (existingTask == null)
        {
            return NotFound();
        }

        existingTask.TicketNumber = updatedTask.TicketNumber;
        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.IsCompleted = updatedTask.IsCompleted;
        existingTask.DueDate = updatedTask.DueDate;
        existingTask.Priority = updatedTask.Priority;
        existingTask.Assignee = updatedTask.Assignee;

        return NoContent();
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }
        tasks.Remove(task);
        return NoContent();
    }
}
