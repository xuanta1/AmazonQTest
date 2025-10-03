#nullable disable
#pragma warning disable CS8618
#pragma warning disable CA1031

using TestAmazonQ.Constants;
using TestAmazonQ.Models;
using TestAmazonQ.Models.Responses;
using TestAmazonQ.Repositories.Interfaces;

namespace TestAmazonQ.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<User>> ValidateUserAsync(string username, string password)
    {
        try
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return ApiResponse<User>.Ok(user, "Login successful");
            }
            return ApiResponse<User>.Unauthorized(Messages.InvalidCredentials);
        }
        catch
        {
            return ApiResponse<User>.InternalServerError("Login failed");
        }
    }

    public async Task<ApiResponse<User>> CreateUserAsync(string username, string password)
    {
        try
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            if (existingUser != null)
            {
                return ApiResponse<User>.BadRequest(Messages.UsernameAlreadyExists);
            }

            var user = new User
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            var createdUser = await _userRepository.AddAsync(user);
            return ApiResponse<User>.Ok(createdUser, Messages.UserCreatedSuccessfully);
        }
        catch
        {
            return ApiResponse<User>.InternalServerError(Messages.FailedToCreateUser);
        }
    }

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