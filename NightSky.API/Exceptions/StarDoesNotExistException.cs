using NightSky.API.Constants;

namespace NightSky.API.Exceptions;

public class StarDoesNotExistException : BaseNightSkyException
{
    public StarDoesNotExistException() : base(e =>
    {
        e.AddError(Langs.English, "Star with provided id does not exist")
            .AddError(Langs.Polish, "Gwiazda o podanym id nie istnieje");
    })
    {
        StatusCode = StatusCodes.Status404NotFound;
    }
}