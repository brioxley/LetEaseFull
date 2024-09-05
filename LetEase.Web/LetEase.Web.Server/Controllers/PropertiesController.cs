using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PropertiesController : ControllerBase
{
	[HttpGet]
	public IActionResult Get()
	{
		return Ok(new[] { new { Id = 1, Name = "Test Property" } });
	}
}
