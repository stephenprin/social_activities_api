using System.ComponentModel.DataAnnotations;

namespace Application.Activities.DTOs;

public class CreateActivityDto
{
   
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    public string Description { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
   
    public string City { get; set; } = string.Empty;
    

    public double Latitude { get; set; } = 0;
      public double Longitude { get; set; } = 0;
}
