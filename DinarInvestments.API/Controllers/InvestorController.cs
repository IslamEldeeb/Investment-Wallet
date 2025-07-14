using DinarInvestments.Application.Dtos;
using DinarInvestments.Application.Dtos.Investors;
using DinarInvestments.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DinarInvestments.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvestorController(IInvestorService investorService) : ControllerBase
{
    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllInvestors()
    {
        var investors = await investorService.GetAllInvestors();
        return Ok(investors);
    }


    [HttpPost("create")]
    public async Task<IActionResult> CreateInvestor([FromBody] CreateInvestorDto input)
    {
        await investorService.CreateInvestor(input);
        return Ok(new { Message = "Investor created successfully." });
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateInvestor(long id, [FromBody] UpdateInvestorInfo input)
    {
        await investorService.UpdateInvestor(id, input);
        return Ok(new { Message = "Investor updated successfully." });
    }
    
    [HttpPost("fundWallet")]
    public async Task<IActionResult> FundInvestorWallet([FromBody] FundInvestorWallet input)
    {
        await investorService.FundInvestorWallet(input);
        return Ok(new { Message = "Investor wallet funded successfully." });
    }
}