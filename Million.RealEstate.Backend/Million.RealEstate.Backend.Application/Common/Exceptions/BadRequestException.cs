namespace Million.RealEstate.Backend.Application.Common.Exceptions;

public class BadRequestException : AppException
{
    public BadRequestException(string message)
         : base(message, 400)
    {
    }

    public BadRequestException(string message, object additionalData)
        : base(message, 400, additionalData)  // ← Pasar al constructor base
    {
    }

    public BadRequestException(string message, string errorCode)
        : base(message, 400)
    {
        // Ahora sí se puede asignar porque tiene set;
        AdditionalData = new { errorCode };
    }
}
