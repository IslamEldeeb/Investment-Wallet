using DinarInvestments.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DinarInvestments.API.Controllers;



[ApiController]
[Route("api/[controller]")]
public class InvestmentController(IInvestmentService investmentService) : ControllerBase
{
    
}