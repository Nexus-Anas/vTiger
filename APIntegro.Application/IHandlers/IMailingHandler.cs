using APIntegro.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace APIntegro.Application.IHandlers;

public interface IMailingHandler
{
    Task SendEmail(Email email);
}