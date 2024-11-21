using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TMS.Models;

namespace TMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly List<ProjectClass> _projects;
        private int _nextId;

        public ProjectController(List<ProjectClass>? projects = null)
        {
            _projects = projects ?? new List<ProjectClass>();
            _nextId = _projects.Count > 0 ? _projects.Max(p => p.Id) + 1 : 1;
        }

        // Get all projects
        [HttpGet]
        public ActionResult<IEnumerable<ProjectClass>> GetProjects()
        {
            return Ok(_projects);
        }

        // Get a project by ID
        [HttpGet("{id}")]
        public ActionResult<ProjectClass> GetProject(int id)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();
            return Ok(project);
        }

        // Create a new project
        [HttpPost]
        public ActionResult<ProjectClass> CreateProject(ProjectClass project)
        {
            project.Id = _nextId++;
            _projects.Add(project);
            return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
        }

        // Update an existing project
        [HttpPut("{id}")]
        public ActionResult<ProjectClass> UpdateProject(int id, ProjectClass updatedProject)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
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
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null) return NotFound();

            _projects.Remove(project);
            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult<ProjectClass> UpdateProjectById(int id, [FromBody] ProjectClass updatedProject)
        {
            var project = _projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
                return NotFound();

            project.Name = updatedProject.Name;
            project.Description = updatedProject.Description;
            project.StartDate = updatedProject.StartDate;
            project.EndDate = updatedProject.EndDate;

            return Ok(project);
        }
    }
}
