#nullable disable

namespace TestAmazonQ.Constants;

/// <summary>
/// Application message constants
/// </summary>
public static class Messages
{
    // Auth Messages
    public const string InvalidCredentials = "Invalid credentials";
    public const string LoggedOutSuccessfully = "Logged out successfully";
    
    // User Management Messages
    public const string UsernameAlreadyExists = "Username already exists";
    public const string UserCreatedSuccessfully = "User created successfully";
    public const string UserUpdatedSuccessfully = "User updated successfully";
    public const string UserDeletedSuccessfully = "User deleted successfully";
    public const string FailedToCreateUser = "Failed to create user";
    public const string FailedToUpdateUser = "Failed to update user";
    public const string FailedToDeleteUser = "Failed to delete user";
}