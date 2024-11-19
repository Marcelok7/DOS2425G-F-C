using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TMS.Controller;
using TMS.Models;
using Xunit;

namespace XunitTest.ControllerTest
{
    public class UserControllerTest
    {
        /* ======================================================================== GET ALL USERS ======================================================================== */
        [Fact]
        public void GetAllUsers_ReturnsOkWithUsers()
        {
            // Arrange
            var userController = new UserController();

            // Act
            var result = userController.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var users = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.True(users.Any());
        }

        /* ======================================================================== GET USER ======================================================================== */
        [Fact]
        public void GetUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var userController = new UserController();
            int nonExistentId = 9999;

            // Act
            var result = userController.GetUser(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetUser_UserFound_ReturnsOkWithUser()
        {
            // Arrange
            var userController = new UserController();
            int existentId = 1;

            // Act
            var result = userController.GetUser(existentId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var user = Assert.IsType<User>(okResult.Value);
            Assert.Equal(existentId, user.ID);
        }

        /* ======================================================================== CREATE USER ======================================================================== */
        [Fact]
        public void CreateUser_ValidUser_ReturnsCreatedAtAction()
        {
            // Arrange
            var userController = new UserController();
            var newUser = new User
            {
                UserName = "newuser",
                Email = "newuser@example.com",
                FullName = "New User",
                UserRole = "User"
            };

            // Act
            var result = userController.CreateUser(newUser);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdUser = Assert.IsType<User>(createdResult.Value);
            Assert.Equal(newUser.UserName, createdUser.UserName);
            Assert.Equal(newUser.Email, createdUser.Email);
        }

        /* ======================================================================== UPDATE USER ======================================================================== */
        [Fact]
        public void UpdateUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var userController = new UserController();
            int nonExistentId = 9999;
            var updatedUser = new User
            {
                ID = nonExistentId,
                UserName = "updateduser",
                Email = "updateduser@example.com",
                FullName = "Updated User",
                UserRole = "Admin"
            };

            // Act
            var result = userController.UpdateUser(nonExistentId, updatedUser);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void UpdateUser_ValidRequest_ReturnsNoContent()
        {
            // Arrange
            var userController = new UserController();

            // Garante que o usuário existe na lista
            var existingUser = new User
            {
                ID = 1,
                UserName = "existinguser",
                Email = "existinguser@example.com",
                FullName = "Existing User",
                UserRole = "Admin"
            };
            userController.CreateUser(existingUser); // Adiciona o usuário para testes

            int existentId = existingUser.ID;
            var updatedUser = new User
            {
                ID = existentId,
                UserName = "updateduser",
                Email = "updateduser@example.com",
                FullName = "Updated User",
                UserRole = "User"
            };

            // Act
            var result = userController.UpdateUser(existentId, updatedUser);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


        /* ======================================================================== DELETE USER ======================================================================== */
        [Fact]
        public void DeleteUser_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var userController = new UserController();
            int nonExistentId = 9999;

            // Act
            var result = userController.DeleteUser(nonExistentId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteUser_ValidId_RemovesUserAndReturnsNoContent()
        {
            // Arrange
            var userController = new UserController();
            int existentId = 1;

            // Act
            var result = userController.DeleteUser(existentId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
