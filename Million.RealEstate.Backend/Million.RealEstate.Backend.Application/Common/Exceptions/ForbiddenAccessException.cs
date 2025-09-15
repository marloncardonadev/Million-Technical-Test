namespace Million.RealEstate.Backend.Application.Common.Exceptions;

public class ForbiddenAccessException : AppException
{
    public ForbiddenAccessException()
        : base("Access denied", 403)
    {
    }
}
