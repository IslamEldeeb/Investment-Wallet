using System.ComponentModel.DataAnnotations;

namespace DinarInvestments.Application.Dtos.Investors;

public class CreateInvestorDto
{
    [Required]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
}