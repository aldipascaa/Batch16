
using Microsoft.AspNetCore.Mvc;

namespace HospitalPatient.Services;

public static class ServiceResultExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase c, ServiceResult<T> r)
        => r.StatusCode switch
        {
            System.Net.HttpStatusCode.OK      => c.Ok(r.Data),
            System.Net.HttpStatusCode.Created => c.Created(string.Empty, r.Data),
            System.Net.HttpStatusCode.BadRequest => c.ValidationProblem(),
            System.Net.HttpStatusCode.NotFound => c.NotFound(r.Message),
            _ => c.StatusCode((int)r.StatusCode, r.Message)
        };
}
