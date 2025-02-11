using Twilio.Rest.Api.V2010.Account;

namespace APIntegro.Application.Interfaces;

public interface ISMSService
{
    Task<MessageResource> SendAsync(string message, string to);
}