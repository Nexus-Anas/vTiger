using APIntegro.Application.IHandlers;
using APIntegro.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace APIntegro.Infrastructure.Handlers;

public class MailingHandler : IMailingHandler
{
    private readonly EmailSettings _emailSettings;

    public MailingHandler(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmail(Email email)
    {
        try
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(_emailSettings.Email));
            message.To.Add(MailboxAddress.Parse(email.SendTo));
            message.Subject = email.Subject;

            message.Body = new TextPart("plain")
            {
                Text = email.Body
            };

            // create a SMTP client
            var smtpClient = new SmtpClient();
            smtpClient.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(_emailSettings.Email, _emailSettings.Password);

            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while sending email with MailKit.", ex);
        }
    }

}
