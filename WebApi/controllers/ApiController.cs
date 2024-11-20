using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ApiController : ControllerBase
{
    [HttpGet("piscines")]
    public IActionResult GetWithParameter([FromQuery] string arrondissement)
    {
        return Ok(arrondissement);
    }
}