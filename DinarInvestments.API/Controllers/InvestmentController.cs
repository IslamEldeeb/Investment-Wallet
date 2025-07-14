using DinarInvestments.Application.Dtos.Investments;
using DinarInvestments.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DinarInvestments.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestmentController(IInvestmentService investmentService) : ControllerBase
{
    [HttpPost("invest")]
    public async Task<IActionResult> InvestToOpportunity([FromBody] InvestToOpportunityDto input)
    {
        await investmentService.AddInvestment(input);
        return Ok(new { Message = "Investment added successfully." });
    }
}