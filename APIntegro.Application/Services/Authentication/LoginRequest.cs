using System.ComponentModel.DataAnnotations;

namespace APIntegro.Application.Services.Authentication;

public record LoginRequest(
        [Required]
        string UserName,
        [Required]
        string AccessKey
    );