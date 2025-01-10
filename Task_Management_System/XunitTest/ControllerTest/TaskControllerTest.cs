using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TMS.Models;

namespace XunitTest.ControllerTest
{
    public class TaskControllerTest
    {
        private List<TaskItem> tasks;
        private TaskController controller;

        public TaskControllerTest()
        {
            tasks = new List<TaskItem>
            {
                new TaskItem { Id = 1, TicketNumber = "JS-1203", Title = "Bug reported - fix", Description = "A bug was detected in Service X", IsCompleted = false, Priority = "High", Assignee = "John Doe", DueDate = DateTime.Now.AddDays(2) },
                new TaskItem { Id = 2, TicketNumber = "AB-352", Title = "New Functionality - use C#", Description = "It's necessary to use .NET Core in these lessons", IsCompleted = true, Priority = "Medium", Assignee = "Jane Smith", DueDate = DateTime.Now.AddDays(-1) }
            };
            controller = new TaskController(tasks);
        }

        /* ======================================================================== GET TASKS ======================================================================== */

        [Fact]
        public void GetTasks_ReturnsAllTasks()
        {
            var result = controller.GetTasks() as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var taskList = result.Value as List<TaskItem>;
            Assert.Equal(2, taskList.Count);
        }

        [Fact]
        public void GetTask_ExistingId_ReturnsTask()
        {
            var result = controller.GetTask(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var task = result.Value as TaskItem;
            Assert.NotNull(task);
            Assert.Equal(1, task.Id);
        }

        [Fact]
        public void GetTask_NonExistingId_ReturnsNotFound()
        {
            var result = controller.GetTask(99);

            Assert.IsType<NotFoundResult>(result);
        }

        /* ======================================================================== CREATE TASKS ======================================================================== */

        [Fact]
        public void CreateTask_ValidTask_ReturnsCreatedAtAction()
        {
            var newTask = new TaskItem
            {
                TicketNumber = "JS-1234",
                Title = "New Task",
                Description = "New Description",
                IsCompleted = false,
                Priority = "High",
                Assignee = "John Doe",
                DueDate = DateTime.Now.AddDays(3)
            };

            var result = controller.CreateTask(newTask) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(201, result.StatusCode);
            Assert.NotNull(result.RouteValues["id"]);
            var createdTask = result.Value as TaskItem;
            Assert.NotNull(createdTask);
            Assert.Equal("JS-1234", createdTask.TicketNumber);
            Assert.Equal("New Task", createdTask.Title);
            Assert.Equal("John Doe", createdTask.Assignee);
            Assert.Equal("High", createdTask.Priority);
        }

        [Fact]
        public void CreateTask_NullTask_ReturnsBadRequest()
        {
            var result = controller.CreateTask(null);

            Assert.IsType<BadRequestResult>(result);
        }

        /* ======================================================================== UPDATE TASKS ======================================================================== */

        [Fact]
        public void UpdateTask_ValidTask_ReturnsNoContent()
        {
            var updatedTask = new TaskItem { Id = 1, TicketNumber = "JS-1203", Title = "Updated Task", Description = "Updated Description", IsCompleted = true, Priority = "High", Assignee = "John Doe", DueDate = DateTime.Now.AddDays(5) };

            var result = controller.UpdateTask(updatedTask);

            Assert.IsType<NoContentResult>(result);
            var task = tasks.FirstOrDefault(t => t.Id == 1);
            Assert.NotNull(task);
            Assert.Equal("Updated Task", task.Title);
            Assert.True(task.IsCompleted);
        }

        [Fact]
        public void UpdateTask_NonExistingId_ReturnsNotFound()
        {
            var updatedTask = new TaskItem { Id = 99, Title = "Non-Existing Task" };

            var result = controller.UpdateTask(updatedTask);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateTask_NullTask_ReturnsBadRequest()
        {
            var result = controller.UpdateTask(null);

            Assert.IsType<BadRequestResult>(result);
        }

        /* ======================================================================== DELETE TASKS ======================================================================== */

        [Fact]
        public void DeleteTask_ExistingId_ReturnsNoContent()
        {
            var result = controller.DeleteTask(1);

            Assert.IsType<NoContentResult>(result);
            Assert.Null(tasks.FirstOrDefault(t => t.Id == 1));
        }

        [Fact]
        public void DeleteTask_NonExistingId_ReturnsNotFound()
        {
            var result = controller.DeleteTask(99);

            Assert.IsType<NotFoundResult>(result);
        }
    }

    // Mock de controlador
    public class TaskController : ControllerBase
    {
        private List<TaskItem> tasks;

        public TaskController(List<TaskItem> tasks)
        {
            this.tasks = tasks;
        }

        public IActionResult GetTasks() => Ok(tasks);

        public IActionResult GetTask(int id)
        {
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

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
            existingTask.Priority = updatedTask.Priority;
            existingTask.Assignee = updatedTask.Assignee;
            existingTask.DueDate = updatedTask.DueDate;

            return NoContent();
        }

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
}
