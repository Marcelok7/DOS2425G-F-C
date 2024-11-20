using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;
using TMS.Controller;
using TMS.Models;

namespace XunitTest.ControllerTest
{
    public class ProjectsControllerTests
    {
        private ProjectController CreateControllerWithMockData()
        {
            var mockProjects = new List<ProjectClass>
            {
                new ProjectClass { Id = 1, Name = "Soccer For Fun", Description = "Futebol", StartDate = DateOnly.FromDateTime(DateTime.UtcNow), EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)) },
                new ProjectClass { Id = 2, Name = "Just for Fun", Description = "Tenis", StartDate = DateOnly.FromDateTime(DateTime.UtcNow), EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)) },
                new ProjectClass { Id = 3, Name = "For the Win", Description = "Vólei", StartDate = DateOnly.FromDateTime(DateTime.UtcNow), EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)) }
            };
            return new ProjectController(mockProjects);
        }

        [Fact]
        public void GetProjects_ShouldReturnAllProjects()
        {
            // Arrange
            var controller = CreateControllerWithMockData();

            // Act
            var result = controller.GetProjects();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<ProjectClass>>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var projects = Assert.IsType<List<ProjectClass>>(okResult.Value);
            Assert.Equal(3, projects.Count);
        }

        [Fact]
        public void GetProject_ExistingId_ShouldReturnProject()
        {
            // Arrange
            var controller = CreateControllerWithMockData();

            // Act
            var result = controller.GetProject(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProjectClass>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var project = Assert.IsType<ProjectClass>(okResult.Value);
            Assert.Equal(1, project.Id);
        }

        [Fact]
        public void CreateProject_ValidProject_ShouldReturnCreatedProject()
        {
            // Arrange
            var controller = CreateControllerWithMockData();
            var newProject = new ProjectClass
            {
                Name = "New Project",
                Description = "New Description",
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(10))
            };

            // Act
            var result = controller.CreateProject(newProject);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProjectClass>>(result);
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var createdProject = Assert.IsType<ProjectClass>(createdAtResult.Value);

            Assert.Equal(4, createdProject.Id); // O próximo ID agora será 4
            Assert.Equal("New Project", createdProject.Name);
        }

        [Fact]
        public void DeleteProject_ExistingId_ShouldReturnNoContent()
        {
            // Arrange
            var controller = CreateControllerWithMockData();

            // Act
            var result = controller.DeleteProject(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void UpdateProjectById_ExistingId_ShouldReturnUpdatedProject()
        {
            // Arrange
            var controller = CreateControllerWithMockData();
            var updatedProject = new ProjectClass
            {
                Name = "Updated Project",
                Description = "Updated Description",
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(10))
            };

            // Act
            var result = controller.UpdateProjectById(1, updatedProject);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProjectClass>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var project = Assert.IsType<ProjectClass>(okResult.Value);

            Assert.Equal("Updated Project", project.Name);
            Assert.Equal("Updated Description", project.Description);
            Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow.Date), project.StartDate);
            Assert.Equal(DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(10)), project.EndDate);
        }

        [Fact]
        public void UpdateProjectById_NonExistingId_ShouldReturnNotFound()
        {
            // Arrange
            var controller = CreateControllerWithMockData();
            var updatedProject = new ProjectClass
            {
                Name = "Updated Project",
                Description = "Updated Description",
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(10))
            };

            // Act
            var result = controller.UpdateProjectById(99, updatedProject);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProjectClass>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
    }
}
