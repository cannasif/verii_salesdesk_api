using salesdesk_api.Modules.SmtpIntegration.Application.Dtos.Mail;
using salesdesk_api.Modules.SmtpIntegration.Application.Services;
using Hangfire;
using Infrastructure.BackgroundJobs.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace salesdesk_api.Modules.SmtpIntegration.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MailController : ControllerBase
{
    private readonly IMailService _mailService;
    private readonly ILogger<MailController> _logger;
    private readonly ILocalizationService _localizationService;

    public MailController(
        IMailService mailService,
        ILogger<MailController> logger,
        ILocalizationService localizationService)
    {
        _mailService = mailService;
        _logger = logger;
        _localizationService = localizationService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] SendMailDto dto)
    {
        try
        {
            var result = await _mailService.SendEmailAsync(
                dto.To,
                dto.Subject,
                dto.Body,
                dto.FromEmail,
                dto.FromName,
                dto.IsHtml,
                dto.Cc,
                dto.Bcc,
                dto.Attachments
            );

            if (result)
            {
                return Ok(new { message = "Email sent successfully", success = true });
            }

            return BadRequest(new { message = "Failed to send email", success = false });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email");
            return StatusCode(500, new { message = "An error occurred while sending email", error = ex.Message });
        }
    }

    [HttpPost("send-test")]
    public async Task<ActionResult<ApiResponse<bool>>> SendTest([FromBody] SendTestMailDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
        {
            var unauth = ApiResponse<bool>.ErrorResult(
                _localizationService.GetLocalizedString("General.Unauthorized"),
                _localizationService.GetLocalizedString("General.Unauthorized"),
                StatusCodes.Status401Unauthorized);
            return StatusCode(unauth.StatusCode, unauth);
        }

        var response = await _mailService.SendTestEmailAsync(dto, userId).ConfigureAwait(false);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("send-async")]
    public IActionResult SendEmailAsync([FromBody] SendMailDto dto)
    {
        try
        {
            var jobId = BackgroundJob.Enqueue<IMailJob>(job =>
                job.SendEmailWithAttachmentsAsync(
                    dto.To,
                    dto.Subject,
                    dto.Body,
                    dto.FromEmail,
                    dto.FromName,
                    dto.IsHtml,
                    dto.Cc,
                    dto.Bcc,
                    dto.Attachments
                )
            );

            return Ok(new
            {
                message = "Email queued for sending",
                success = true,
                jobId = jobId
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error queuing email");
            return StatusCode(500, new { message = "An error occurred while queuing email", error = ex.Message });
        }
    }

    [HttpPost("send-bulk")]
    public IActionResult SendBulkEmail([FromBody] BulkSendMailDto dto)
    {
        try
        {
            var jobIds = new List<string>();

            foreach (var recipient in dto.To)
            {
                var jobId = BackgroundJob.Enqueue<IMailJob>(job =>
                    job.SendEmailWithAttachmentsAsync(
                        recipient,
                        dto.Subject,
                        dto.Body,
                        dto.FromEmail,
                        dto.FromName,
                        dto.IsHtml,
                        dto.Cc,
                        dto.Bcc,
                        dto.Attachments
                    )
                );
                jobIds.Add(jobId);
            }

            return Ok(new
            {
                message = $"{dto.To.Count} emails queued for sending",
                success = true,
                jobIds = jobIds,
                count = jobIds.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error queuing bulk emails");
            return StatusCode(500, new { message = "An error occurred while queuing emails", error = ex.Message });
        }
    }

    [HttpPost("schedule")]
    public IActionResult ScheduleEmail([FromBody] SendMailDto dto, [FromQuery] DateTime scheduleAt)
    {
        try
        {
            if (scheduleAt <= DateTime.UtcNow)
            {
                return BadRequest(new { message = "Schedule time must be in the future", success = false });
            }

            var delay = scheduleAt - DateTime.UtcNow;
            var jobId = BackgroundJob.Schedule<IMailJob>(job =>
                job.SendEmailWithAttachmentsAsync(
                    dto.To,
                    dto.Subject,
                    dto.Body,
                    dto.FromEmail,
                    dto.FromName,
                    dto.IsHtml,
                    dto.Cc,
                    dto.Bcc,
                    dto.Attachments
                ),
                delay
            );

            return Ok(new
            {
                message = "Email scheduled for sending",
                success = true,
                jobId = jobId,
                scheduledAt = scheduleAt
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error scheduling email");
            return StatusCode(500, new { message = "An error occurred while scheduling email", error = ex.Message });
        }
    }
}
