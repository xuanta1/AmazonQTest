#nullable disable

namespace TestAmazonQ.Models.Responses;

/// <summary>
/// Generic API response wrapper
/// </summary>
/// <typeparam name="T">Type of data being returned</typeparam>
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    /// <summary>
    /// Creates a successful response
    /// </summary>
    /// <param name="data">Response data</param>
    /// <param name="message">Success message</param>
    /// <returns>Success API response</returns>
    public static ApiResponse<T> Ok(T data, string message = "Success")
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates a bad request response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <returns>Bad request API response</returns>
    public static ApiResponse<T> BadRequest(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 400,
            Message = message
        };
    }

    /// <summary>
    /// Creates an unauthorized response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <returns>Unauthorized API response</returns>
    public static ApiResponse<T> Unauthorized(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 401,
            Message = message
        };
    }

    /// <summary>
    /// Creates a forbidden response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <returns>Forbidden API response</returns>
    public static ApiResponse<T> Forbidden(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 403,
            Message = message
        };
    }

    /// <summary>
    /// Creates a not found response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <returns>Not found API response</returns>
    public static ApiResponse<T> NotFound(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 404,
            Message = message
        };
    }

    /// <summary>
    /// Creates an internal server error response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <returns>Internal server error API response</returns>
    public static ApiResponse<T> InternalServerError(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 500,
            Message = message
        };
    }
}