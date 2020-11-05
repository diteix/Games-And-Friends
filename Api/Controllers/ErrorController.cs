using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class ErrorController : ControllerBase
{
    [Route("error")]
    public IActionResult Error() 
    {
        return  Problem();
    }
}