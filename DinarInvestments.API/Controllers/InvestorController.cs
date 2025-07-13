using Microsoft.AspNetCore.Mvc;

namespace DinarInvestments.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestorController : ControllerBase
{
    
    [HttpGet("test")]
    public IActionResult GetInvestors()
    {
        // This is a placeholder for the actual implementation.
        // In a real application, you would retrieve the list of investors from a database or service.
        return Ok(new { Message = "List of investors" });
    }
    
}