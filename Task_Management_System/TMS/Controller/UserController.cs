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
                ID = 1,
                UserName = "joaquimlano",
                Email = "joaquim.lano@example.com",
                FullName = "Joaquim Lano",
                UserRole = "Admin"
            }
        };

        // GET: api/user
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(users);
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = users.FirstOrDefault(u => u.ID == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // POST: api/user
        [HttpPost]
        public IActionResult CreateUser(User newUser)
        {
            // Gera um novo ID com base no último usuário da lista
            newUser.ID = users.Max(u => u.ID) + 1;
            users.Add(newUser);
            return CreatedAtAction(nameof(GetUser), new { id = newUser.ID }, newUser);
        }

        // PUT: api/user/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = users.FirstOrDefault(u => u.ID == id);
            if (user == null) return NotFound();

            // Atualiza os campos do usuário
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.FullName = updatedUser.FullName;
            user.UserRole = updatedUser.UserRole;

            return NoContent();
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = users.FirstOrDefault(u => u.ID == id);
            if (user == null) return NotFound();

            users.Remove(user);
            return NoContent();
        }
    }
}
