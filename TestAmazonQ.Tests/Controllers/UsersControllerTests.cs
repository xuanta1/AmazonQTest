using Microsoft.AspNetCore.Mvc;
using Moq;
using TestAmazonQ.Controllers;
using TestAmazonQ.Models;
using TestAmazonQ.Models.Requests;
using TestAmazonQ.Models.Responses;
using TestAmazonQ.Repositories.Interfaces;
using TestAmazonQ.Services;
using Xunit;

namespace TestAmazonQ.Tests.Controllers;

public class UsersControllerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UserService _userService;
    private readonly UsersController _controller;

    public UsersControllerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
        _controller = new UsersController(_userService);
    }

    [Fact]
    public async Task GetPaged_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var users = new List<User> { new User { Id = 1, Username = "test" } };
        var pagedResult = new PagedResult<User>
        {
            Items = users,
            TotalCount = 1,
            PageNumber = 1,
            PageSize = 10
        };
        _mockUserRepository.Setup(x => x.GetPagedAsync(1, 10)).ReturnsAsync(pagedResult);

        // Act
        var result = await _controller.GetPaged(1, 10);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsOkResult_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test" };
        _mockUserRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenUserNotExists()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, statusResult.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var request = new CreateUserRequest { Username = "test", Password = "password" };
        var user = new User { Id = 1, Username = "test" };
        _mockUserRepository.Setup(x => x.GetByUsernameAsync("test")).ReturnsAsync((User)null);
        _mockUserRepository.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsBadRequest_WhenUsernameExists()
    {
        // Arrange
        var request = new CreateUserRequest { Username = "test", Password = "password" };
        var existingUser = new User { Id = 1, Username = "test" };
        _mockUserRepository.Setup(x => x.GetByUsernameAsync("test")).ReturnsAsync(existingUser);

        // Act
        var result = await _controller.Create(request);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(400, statusResult.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = "updated", Password = "newpassword" };
        var user = new User { Id = 1, Username = "test" };
        _mockUserRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);
        _mockUserRepository.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(user);

        // Act
        var result = await _controller.Update(1, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenUserNotExists()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = "updated" };
        _mockUserRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.Update(1, request);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, statusResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsOkResult_WhenSuccessful()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test" };
        _mockUserRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(user);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenUserNotExists()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync((User)null);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(404, statusResult.StatusCode);
    }

    // ERROR CASES
    [Fact]
    public async Task GetPaged_ReturnsInternalServerError_WhenRepositoryThrowsException()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetPagedAsync(1, 10))
            .ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _controller.GetPaged(1, 10);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }

    [Fact]
    public async Task GetById_ReturnsInternalServerError_WhenRepositoryThrowsException()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetByIdAsync(1))
            .ThrowsAsync(new Exception("Database connection failed"));

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }

    [Fact]
    public async Task Create_ReturnsInternalServerError_WhenRepositoryThrowsException()
    {
        // Arrange
        var request = new CreateUserRequest { Username = "test", Password = "password" };
        _mockUserRepository.Setup(x => x.GetByUsernameAsync("test"))
            .ThrowsAsync(new Exception("Database timeout"));

        // Act
        var result = await _controller.Create(request);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsInternalServerError_WhenRepositoryThrowsException()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = "updated" };
        _mockUserRepository.Setup(x => x.GetByIdAsync(1))
            .ThrowsAsync(new Exception("Network error"));

        // Act
        var result = await _controller.Update(1, request);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }

    [Fact]
    public async Task Delete_ReturnsInternalServerError_WhenRepositoryThrowsException()
    {
        // Arrange
        _mockUserRepository.Setup(x => x.GetByIdAsync(1))
            .ThrowsAsync(new Exception("Constraint violation"));

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }
}