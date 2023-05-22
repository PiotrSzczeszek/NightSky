namespace NightSky.API.Models;

public class ConstellationModel
{
    public int ConstallationId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }

    public ICollection<StarModel> Stars;
}