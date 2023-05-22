namespace NightSky.API.Models;

public class StarModel
{
    public int? StarId { get; set; }
    public string StarName { get; set; }
    public string Description { get; set; }
    
    public string ImageUrl { get; set; }
    
    public ICollection<ConstellationModel> Constallations { get; set; }
}