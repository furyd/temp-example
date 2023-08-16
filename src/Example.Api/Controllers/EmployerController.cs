using Example.Api.Services.Implementation;
using Example.Domain.Queries.Interfaces;
using Example.Domain.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployerController : ControllerBase
    {
        private readonly IFieldQueries _fieldQueries;
        private readonly ResultService _resultService;

        public EmployerController(IFieldQueries fieldQueries, ResultService resultService)
        {
            _fieldQueries = fieldQueries;
            _resultService = resultService;
        }

        [HttpGet]
        [Route("{employer:guid}")]
        public async Task<IActionResult> List(Guid employer, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
            => _resultService.List(
                await _fieldQueries.List(new PagedEmployerFilter(page, pageSize, employer)));
    }
}