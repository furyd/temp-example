using Example.Api.Constants;
using Example.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Services.Implementation;

public class ResultService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResultService(IHttpContextAccessor contextAccessor)
    {
        _httpContextAccessor = contextAccessor;
    }

    public IActionResult List<T>(IPager<T> pageItems)
    {
        _httpContextAccessor.HttpContext?.Response.Headers.Add(Headers.Page, pageItems.Page.ToString());
        _httpContextAccessor.HttpContext?.Response.Headers.Add(Headers.PageSize, pageItems.PageSize.ToString());
        _httpContextAccessor.HttpContext?.Response.Headers.Add(Headers.Records, pageItems.ActualPageSize.ToString());
        return List(pageItems.Records);
    }

    public IActionResult List<T>(IEnumerable<T> items) =>
        items.Any() ? new OkObjectResult(items) : new NoContentResult();

    public IActionResult Retrieve<T> (T? item) => 
        item is null ? new NotFoundResult() : new OkObjectResult(item);

    public IActionResult Update()
        => new AcceptedResult();

    public IActionResult Delete()
        => new AcceptedResult();
}