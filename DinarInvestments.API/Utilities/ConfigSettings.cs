using System.ComponentModel.DataAnnotations;

namespace DinarInvestments.API.Utilities;

public class ConfigSettings
{
    [Required] public string ConnectionString { get; set; } 
}
