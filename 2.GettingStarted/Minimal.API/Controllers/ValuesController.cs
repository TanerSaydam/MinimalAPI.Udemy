using Microsoft.AspNetCore.Mvc;

namespace Minimal.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello from controllers");
    }
}
