#nullable disable
#pragma warning disable CS8618
#pragma warning disable CA1031

using TestAmazonQ.Constants;
using TestAmazonQ.Models;
using TestAmazonQ.Models.Responses;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Services;

/// <summary>
/// Service for user management operations
/// </summary>
public class UserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Initializes a new instance of the UserService class
    /// </summary>
    /// <param name="userRepository">User repository instance</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// Validates user credentials
    /// </summary>
    /// <param name="username">Username to validate</param>
    /// <param name="password">Password to validate</param>
    /// <returns>API response with user data if valid</returns>
    public async Task<ApiResponse<User>> ValidateUserAsync(string username, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return ApiResponse<User>.BadRequest("Username and password are required");
            }

            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return ApiResponse<User>.Ok(user, "Login successful");
            }
            return ApiResponse<User>.Unauthorized(Messages.InvalidCredentials);
        }
        catch (Exception)
        {
            return ApiResponse<User>.InternalServerError("Authentication service temporarily unavailable");
        }
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="username">Username for new user</param>
    /// <param name="password">Password for new user</param>
    /// <returns>API response with created user data</returns>
    public async Task<ApiResponse<User>> CreateUserAsync(string username, string password)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return ApiResponse<User>.BadRequest("Username and password are required");
            }

            if (username.Length < 3 || username.Length > 50)
            {
                return ApiResponse<User>.BadRequest("Username must be between 3 and 50 characters");
            }

            if (password.Length < 6 || password.Length > 100)
            {
                return ApiResponse<User>.BadRequest("Password must be between 6 and 100 characters");
            }

            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null)
            {
                return ApiResponse<User>.BadRequest(Messages.UsernameAlreadyExists);
            }

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, 12)
            };

            var createdUser = await _userRepository.AddAsync(user);
            return ApiResponse<User>.Ok(createdUser, Messages.UserCreatedSuccessfully);
        }
        catch (Exception)
        {
            return ApiResponse<User>.InternalServerError(Messages.FailedToCreateUser);
        }
    }

    /// <summary>
    /// Gets user by ID
    /// </summary>
    /// <param name="id">User ID</param>
    /// <returns>API response with user data</returns>
    public async Task<ApiResponse<User>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<User>.NotFound("User not found");
            }
            return ApiResponse<User>.Ok(user);
        }
        catch
        {
            return ApiResponse<User>.InternalServerError("Failed to get user");
        }
    }

    /// <summary>
    /// Gets paginated list of users
    /// </summary>
    /// <param name="pageNumber">Page number</param>
    /// <param name="pageSize">Page size</param>
    /// <returns>API response with paginated user data</returns>
    public async Task<ApiResponse<PagedResult<User>>> GetUsersPagedAsync(int pageNumber, int pageSize)
    {
        try
        {
            var result = await _userRepository.GetPagedAsync(pageNumber, pageSize);
            return ApiResponse<PagedResult<User>>.Ok(result);
        }
        catch
        {
            return ApiResponse<PagedResult<User>>.InternalServerError("Failed to get users");
        }
    }

    /// <summary>
    /// Updates an existing user
    /// </summary>
    /// <param name="id">User ID to update</param>
    /// <param name="username">New username</param>
    /// <param name="password">New password (optional)</param>
    /// <returns>API response with updated user data</returns>
    public async Task<ApiResponse<User>> UpdateUserAsync(int id, string username, string password = null)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<User>.NotFound("User not found");
            }

            user.Username = username;
            if (!string.IsNullOrEmpty(password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
            }

            var updatedUser = await _userRepository.UpdateAsync(user);
            return ApiResponse<User>.Ok(updatedUser, Messages.UserUpdatedSuccessfully);
        }
        catch
        {
            return ApiResponse<User>.InternalServerError(Messages.FailedToUpdateUser);
        }
    }

    /// <summary>
    /// Deletes a user
    /// </summary>
    /// <param name="id">User ID to delete</param>
    /// <returns>API response indicating success</returns>
    public async Task<ApiResponse<bool>> DeleteUserAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return ApiResponse<bool>.NotFound("User not found");
            }

            await _userRepository.DeleteAsync(id);
            return ApiResponse<bool>.Ok(true, Messages.UserDeletedSuccessfully);
        }
        catch
        {
            return ApiResponse<bool>.InternalServerError(Messages.FailedToDeleteUser);
        }
    }
}