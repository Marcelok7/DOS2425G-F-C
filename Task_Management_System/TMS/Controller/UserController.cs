using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMS.Models;

namespace TMS.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static readonly List<User> users = new List<User>
    {
        new User
        {
            Id = 1,
            Nome = "Joaquim Lano",
            Password= "banana",
            
        }
        };


        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(users);
        }

    }

}
