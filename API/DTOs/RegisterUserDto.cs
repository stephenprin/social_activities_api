using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterUserDto
{
    [Required]
    public string DisplayName { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    [StringLength(20, ErrorMessage = "Password must be at least 6 characters long.", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
