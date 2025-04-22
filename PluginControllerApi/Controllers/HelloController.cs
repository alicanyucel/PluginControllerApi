using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]/[action]")]
[ApiController]
[AllowAnonymous]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("Hello from plugin!");
    [HttpGet]
    public IActionResult LoadForm()
    {
        return RedirectToAction("Index", "Home");
    }
}
