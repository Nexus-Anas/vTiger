using System.Net;

namespace APIntegro.Application.Common.Errors;

public class VtigerException : Exception, IServiceException
{
    public string ErrorCode { get; }
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public VtigerException(string errorCode, string errorMessage)
      : base(errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}