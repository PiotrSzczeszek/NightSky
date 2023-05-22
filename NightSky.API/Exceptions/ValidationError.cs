namespace NightSky.API.Exceptions;


public class ValidationError
{
    public string Lang { get; set; }
    public string Message { get; set; }
}

public class ValidationErrorBuilder
{
    private readonly List<ValidationError> _errors = new();

    public ValidationErrorBuilder AddError(string lang, string message)
    {
        _errors.Add(new ValidationError()
        {
            Lang = lang,
            Message = message
        });

        return this;
    }

    public List<ValidationError> Build()
    {
        if (!_errors.Any()) throw new InvalidOperationException();
        return _errors;
    }
}