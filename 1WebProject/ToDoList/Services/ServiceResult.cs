using System.Net;

namespace ToDoList.Services;

public class ServiceResult<T>
{

    public bool Success { get; init; }
    public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
    public string? Message { get; init; }
    public string? ErrorCode { get; init; }
    public T? Data { get; init; }
    public Dictionary<string, string[]>? ValidationErrors { get; init; }

    public static ServiceResult<T> Ok(T data, string? message = null) =>
        new() { Success = true, StatusCode = HttpStatusCode.OK, Data = data, Message = message };

    public static ServiceResult<T> Created(T data, string? message = null) =>
        new() { Success = true, StatusCode = HttpStatusCode.Created, Data = data, Message = message };

    public static ServiceResult<T> NotFound(string? message = null) =>
        new() { Success = false, StatusCode = HttpStatusCode.NotFound, Message = message };

    public static ServiceResult<T> BadRequest(string? message = null, Dictionary<string, string[]>? errors = null) =>
        new() { Success = false, StatusCode = HttpStatusCode.BadRequest, Message = message, ValidationErrors = errors };

    public static ServiceResult<T> Unauthorized(string? message = null) =>
        new() { Success = false, StatusCode = HttpStatusCode.Unauthorized, Message = message };

    public static ServiceResult<T> Forbidden(string? message = null) =>
        new() { Success = false, StatusCode = HttpStatusCode.Forbidden, Message = message };

    public static ServiceResult<T> Error(string? message = null) =>
        new() { Success = false, StatusCode = HttpStatusCode.InternalServerError, Message = message };

}