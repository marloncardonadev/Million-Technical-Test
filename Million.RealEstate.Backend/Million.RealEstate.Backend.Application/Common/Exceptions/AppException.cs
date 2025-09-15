namespace Million.RealEstate.Backend.Application.Common.Exceptions;

public class AppException : Exception
{
    public int StatusCode { get; }
    public object? AdditionalData { get; set; }

    public AppException(string message, int statusCode = 500, object? additionalData = null)
        : base(message)
    {
        StatusCode = statusCode;
        AdditionalData = additionalData;
    }
}
