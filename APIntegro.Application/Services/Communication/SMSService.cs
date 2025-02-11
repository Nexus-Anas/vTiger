using APIntegro.Application.Interfaces;
using APIntegro.Domain.Entities;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace APIntegro.Application.Services.Communication;

public class SMSService : ISMSService
{
    private readonly SMSSettings _smsSettings;

    public SMSService(IOptions<SMSSettings> smsSettings)
    {
        _smsSettings = smsSettings.Value;
    }


    public async Task<MessageResource> SendAsync(string message, string to)
    {
        TwilioClient.Init(_smsSettings.AccountSID, _smsSettings.AuthToken);

        var result = await MessageResource.CreateAsync(
            body: message,
            from: _smsSettings.PhoneNumber,
            to: new PhoneNumber(to)
        );
        return result;
    }
}