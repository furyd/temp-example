using Microsoft.AspNetCore.Mvc;
using Example.Domain.Repositories.Interfaces;

namespace Example.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MultipleController : Controller
    {
        private readonly IMultipleRepository _repository;

        public MultipleController(IMultipleRepository multipleRepository)
        {
            _repository = multipleRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _repository.Multiple());
        }
    }
}
