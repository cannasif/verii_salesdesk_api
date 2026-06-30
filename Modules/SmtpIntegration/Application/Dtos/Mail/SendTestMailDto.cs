using System.ComponentModel.DataAnnotations;

namespace salesdesk_api.Modules.SmtpIntegration.Application.Dtos.Mail;

public class SendTestMailDto
{
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? To { get; set; }
}
