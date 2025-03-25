using System.ComponentModel.DataAnnotations;

namespace Application.Activities.DTOs;

public class CreateActivityDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Category { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string Venue { get; set; } = string.Empty;
    [Required]
    public double Latitude { get; set; } = 0;
    [Required]
    public double Longitude { get; set; } = 0;
}
