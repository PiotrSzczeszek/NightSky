namespace NightSky.API.DAL.Entities;

public class SkyData
{
    public int SkyId { get; set; }
    public int FogLevel { get; set; }
    public int CloudsLevel { get; set; }
    public PrecipitationTypes PrecipitationType { get; set; }
    public DateTime DataDate { get; set; }
}

public enum PrecipitationTypes
{
    None,
    Rain,
    Snow,
    Sleet, // śnieg z deszczem
    Hail // grad
}