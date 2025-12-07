
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Services;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, ServiceResult<T> result)
    {
        return result.StatusCode switch
        {
            System.Net.HttpStatusCode.OK => controller.Ok(result.Data),
            System.Net.HttpStatusCode.Created => controller.Created(string.Empty, result.Data),
            System.Net.HttpStatusCode.BadRequest => controller.ValidationProblem(),
            System.Net.HttpStatusCode.NotFound => controller.NotFound(result.Message),
            System.Net.HttpStatusCode.Unauthorized => controller.Unauthorized(),
            System.Net.HttpStatusCode.Forbidden => controller.Forbid(),
            _ => controller.StatusCode((int)result.StatusCode, result.Message)
        };
    }
}
