namespace NightSky.API.DAL.Entities;

public class Constellation
{
    public int ConstallationId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    
    public virtual ICollection<Star> Stars { get; set; }
}