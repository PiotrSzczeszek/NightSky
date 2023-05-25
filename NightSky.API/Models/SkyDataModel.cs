using System.Text.Json.Serialization;
using NightSky.API.DAL.Entities;

namespace NightSky.API.Models;

public class SkyDataModel
{
    public int? SkyId { get; set; }
    public int FogLevel { get; set; }
    public int CloudsLevel { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PrecipitationTypes PrecipitationType { get; set; }
    
    public DateTime DataDate { get; set; }
}