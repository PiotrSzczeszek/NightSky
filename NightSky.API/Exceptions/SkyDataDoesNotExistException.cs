using NightSky.API.Constants;

namespace NightSky.API.Exceptions;

public class SkyDataDoesNotExistException : BaseNightSkyException
{
    public SkyDataDoesNotExistException() : base(e =>
    {
        e.AddError(Langs.English, "Sky data with provided id does not exist")
            .AddError(Langs.Polish, "Dane nieba o podanym id nie istnieją");
    })
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}