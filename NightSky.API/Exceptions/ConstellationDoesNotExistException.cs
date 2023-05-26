using NightSky.API.Constants;

namespace NightSky.API.Exceptions;

public class ConstellationDoesNotExistException : BaseNightSkyException
{
    public ConstellationDoesNotExistException() : base(e =>
    {
        e.AddError(Langs.English, "Constellation with provided id does not exist")
            .AddError(Langs.Polish, "Konstelacja o podanym id nie istnieje");
    })
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}