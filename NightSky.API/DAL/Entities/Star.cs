namespace NightSky.API.DAL.Entities;

public class Star
{
    public int StarId { get; set; }
    public string StarName { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public virtual ICollection<Constellation> Constallations { get; set; }
}
