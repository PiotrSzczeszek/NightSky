namespace NightSky.API.Exceptions;

public class BaseNightSkyException : Exception
{
    public List<ValidationError> Errors { get; protected set; }
    public int StatusCode { get; protected init; } = 400;
    public BaseNightSkyException(string error) : base(error)
    {
        Errors = new List<ValidationError>();
    }

    public BaseNightSkyException(string error, Action<ValidationErrorBuilder> errorsAction) : base(error)
    {
        var builder = new ValidationErrorBuilder();
        errorsAction.Invoke(builder);

        Errors = builder.Build();
    }

    public BaseNightSkyException(Action<ValidationErrorBuilder> errorsAction)
    {
        var builder = new ValidationErrorBuilder();
        errorsAction.Invoke(builder);

        Errors = builder.Build();
    }
}


