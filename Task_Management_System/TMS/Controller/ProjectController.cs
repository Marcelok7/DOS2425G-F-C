using Microsoft.AspNetCore.Mvc;
using TMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private static readonly List<ProjectClass> projects = new List<ProjectClass>
        {
            new ProjectClass
            {
                Id = 1,
                Name = "Soccer For Fun",
                Description = "Futebol",
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(1))
            },

            new ProjectClass
            {
                Id = 2,
                Name = "Just for Fun",
                Description = "Tenis",
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(1))
            }
        };

        private static int nextId = 3; // Começar a partir do próximo ID

        // Get all projects
        [HttpGet]
        public ActionResult<IEnumerable<ProjectClass>> GetProjects()
        {
            return Ok(projects);
        }

        // Get a project by ID
        [HttpGet("{id}")]
        public ActionResult<ProjectClass> GetProject(int id)
        {
            var project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        // Create a new project
        [HttpPost]
        public ActionResult<ProjectClass> CreateProject(ProjectClass project)
        {
            project.Id = nextId++;
            projects.Add(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // Update an existing project
        [HttpPut("{id}")]
        public ActionResult<ProjectClass> UpdateProject(int id, ProjectClass updatedProject)
        {
            var project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();

            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            project.StartDate = updatedProject.StartDate;
            project.EndDate = updatedProject.EndDate;

            return Ok(project);
        }

        // Delete a project
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();

            projects.Remove(project);
            return NoContent();
        }
    }
}

