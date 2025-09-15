namespace Million.RealEstate.Backend.Application.Common.Exceptions;

public class ValidationException : AppException
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("Validation failures have occurred.", 400)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : this()
    {
        Errors = errors;
    }
}
