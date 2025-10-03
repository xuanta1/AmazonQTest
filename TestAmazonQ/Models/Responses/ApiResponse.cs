#nullable disable

namespace TestAmazonQ.Models.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

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

    public static ApiResponse<T> BadRequest(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 400,
            Message = message
        };
    }

    public static ApiResponse<T> Unauthorized(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 401,
            Message = message
        };
    }

    public static ApiResponse<T> Forbidden(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 403,
            Message = message
        };
    }

    public static ApiResponse<T> NotFound(string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = 404,
            Message = message
        };
    }

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