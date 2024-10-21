using Microsoft.AspNetCore.Mvc;
using TMS.Models;

namespace TMS.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private static readonly List<ProjectClass> projects = new List<ProjectClass>
        {
        new ProjectClass(1, "Kinhas", "Futebol"),

        new ProjectClass(2, "Moove", "Futebol"),

        new ProjectClass(3, "4linhas", "Futebol"),
        };

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            return Ok(projects);
        }
    }
}

