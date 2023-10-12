using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.BaseAPI
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseAPIController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Get()
        {
            return Ok("Test API!");
        }
    }
}
